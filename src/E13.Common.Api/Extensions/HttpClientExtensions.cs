﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security;
using Microsoft.Identity.Client;

namespace System.Net.Http
{
    /// <summary>
    /// Extensions built based on examples taken from Microsoft AzureAD doc
    /// </summary>
    /// <seealso cref="https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/Username-Password-Authentication"/>
    public static class HttpClientExtensions
    {
        private static readonly string? TenantId = Environment.GetEnvironmentVariable("TokenForAAD_TenantId");
        private static readonly string? PublicClientId = Environment.GetEnvironmentVariable("TokenForAAD_PublicClientId");

        /// <summary>
        /// Scopes to request access to the protected Web API (here Microsoft Graph)
        /// </summary>
        private static string[] Scopes { get; set; } = new string[] { "user.read.all" };

        public static void TokenForAAD(this HttpClient client, string username, SecureString password, string[]? additionalScopes = null)
        {
            ArgumentNullException.ThrowIfNull(client);

            var token = client.GetTokenForAAD(username, password, additionalScopes);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static string GetTokenForAAD(this HttpClient client, string username, SecureString password, string[]? additionalScopes = null)
        {
            ArgumentNullException.ThrowIfNull(client);

            try
            {
                var app = PublicClientApplicationBuilder
                    .Create(PublicClientId)
                    .WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
                    .Build();

                // merge the default graph scope with whatever application specific scope is provided
                var scopeList = new List<string>();
                scopeList.AddRange(Scopes);
                if(additionalScopes != null)
                    scopeList.AddRange(additionalScopes);

                var result = app.AcquireTokenByUsernamePassword(
                    scopeList.ToArray(), 
                    username, 
                    password)
                    .ExecuteAsync().Result;

                return result.AccessToken;
            }
            catch (MsalUiRequiredException ex) when (ex.Message.Contains("AADSTS65001", StringComparison.InvariantCultureIgnoreCase))
            {
                // Here are the kind of error messages you could have, and possible mitigations

                // ------------------------------------------------------------------------
                // MsalUiRequiredException: AADSTS65001: The user or administrator has not consented to use the application
                // with ID '{appId}' named '{appName}'. Send an interactive authorization request for this user and resource.

                // Mitigation: you need to get user consent first. This can be done either statically (through the portal), 
                /// or dynamically (but this requires an interaction with Azure AD, which is not possible with 
                // the username/password flow)
                // Statically: in the portal by doing the following in the "API permissions" tab of the application registration:
                // 1. Click "Add a permission" and add all the delegated permissions corresponding to the scopes you want (for instance
                // User.Read and User.ReadBasic.All)
                // 2. Click "Grant/revoke admin consent for <tenant>") and click "yes".
                // Dynamically, if you are not using .NET Core (which does not have any Web UI) by 
                // calling (once only) AcquireTokenInteractive.
                // remember that Username/password is for public client applications that is desktop/mobile applications.
                // If you are using .NET core or don't want to call AcquireTokenInteractive, you might want to:
                // - use device code flow (See https://aka.ms/msal-net-device-code-flow)
                // - or suggest the user to navigate to a URL to consent: https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={clientId}&response_type=code&scope=user.read
                // ------------------------------------------------------------------------

                // ------------------------------------------------------------------------
                // ErrorCode: invalid_grant
                // SubError: basic_action
                // MsalUiRequiredException: AADSTS50079: The user is required to use multi-factor authentication.
                // The tenant admin for your organization has chosen to oblige users to perform multi-factor authentication.
                // Mitigation: none for this flow
                // Your application cannot use the Username/Password grant.
                // Like in the previous case, you might want to use an interactive flow (AcquireTokenInteractive()), 
                // or Device Code Flow instead.
                // Note this is one of the reason why using username/password is not recommended;
                // ------------------------------------------------------------------------

                // ------------------------------------------------------------------------
                // ex.ErrorCode: invalid_grant
                // subError: null
                // Message = "AADSTS70002: Error validating credentials.
                // AADSTS50126: Invalid username or password
                // In the case of a managed user (user from an Azure AD tenant opposed to a
                // federated user, which would be owned
                // in another IdP through ADFS), the user has entered the wrong password
                // Mitigation: ask the user to re-enter the password
                // ------------------------------------------------------------------------

                // ------------------------------------------------------------------------
                // ex.ErrorCode: invalid_grant
                // subError: null
                // MsalServiceException: ADSTS50034: To sign into this application the account must be added to 
                // the {domainName} directory.
                // or The user account does not exist in the {domainName} directory. To sign into this application, 
                // the account must be added to the directory.
                // The user was not found in the directory
                // Explanation: wrong username
                // Mitigation: ask the user to re-enter the username.
                // ------------------------------------------------------------------------
                throw;
            }
            catch (MsalServiceException ex) when (ex.ErrorCode == "invalid_request")
            {
                // ------------------------------------------------------------------------
                // AADSTS90010: The grant type is not supported over the /common or /consumers endpoints. 
                // Please use the /organizations or tenant-specific endpoint.
                // you used common.
                // Mitigation: as explained in the message from Azure AD, the authority you use in the application needs 
                // to be tenanted or otherwise "organizations". change the
                // "Tenant": property in the appsettings.json to be a GUID (tenant Id), or domain name (contoso.com) 
                // if such a domain is registered with your tenant
                // or "organizations", if you want this application to sign-in users in any Work and School accounts.
                // ------------------------------------------------------------------------
                throw;
            }
            catch (MsalServiceException ex) when (ex.ErrorCode == "unauthorized_client")
            {
                // ------------------------------------------------------------------------
                // AADSTS700016: Application with identifier '{clientId}' was not found in the directory '{domain}'.
                // This can happen if the application has not been installed by the administrator of the tenant or consented 
                // to by any user in the tenant.
                // You may have sent your authentication request to the wrong tenant
                // Cause: The clientId in the appsettings.json might be wrong
                // Mitigation: check the clientId and the app registration
                // ------------------------------------------------------------------------
                throw;
            }
            catch (MsalServiceException ex) when (ex.ErrorCode == "invalid_client")
            {
                // ------------------------------------------------------------------------
                // AADSTS70002: The request body must contain the following parameter: 'client_secret or client_assertion'.
                // Explanation: this can happen if your application was not registered as a public client application in Azure AD
                // Mitigation: in the Azure portal, edit the manifest for your application and set the `allowPublicClient` to `true`
                // ------------------------------------------------------------------------
                throw;
            }
            catch (MsalServiceException)
            {
                throw;
            }
            catch (MsalClientException ex) when (ex.ErrorCode == "unknown_user_type")
            {
                // Message = "Unsupported User Type 'Unknown'. Please see https://aka.ms/msal-net-up"
                // The user is not recognized as a managed user, or a federated user. Azure AD was not
                // able to identify the IdP that needs to process the user
                throw new ArgumentException(
                    message: "U/P: Wrong username",
                    paramName: nameof(username), 
                    ex);
            }
            catch (MsalClientException ex) when (ex.ErrorCode == "user_realm_discovery_failed")
            {
                // The user is not recognized as a managed user, or a federated user. Azure AD was not
                // able to identify the IdP that needs to process the user. That's for instance the case
                // if you use a phone number
                throw new ArgumentException("U/P: Wrong username", ex);
            }
            catch (MsalClientException ex) when (ex.ErrorCode == "unknown_user")
            {
                // the username was probably empty
                // ex.Message = "Could not identify the user logged into the OS. See http://aka.ms/msal-net-iwa for details."
                throw new ArgumentException("U/P: Wrong username", ex);
            }
            catch (MsalClientException ex) when (ex.ErrorCode == "parsing_wstrust_response_failed")
            {
                // ------------------------------------------------------------------------
                // In the case of a Federated user (that is owned by a federated IdP, as opposed to a managed user owned in an Azure AD tenant)
                // ID3242: The security token could not be authenticated or authorized.
                // The user does not exist or has entered the wrong password
                // ------------------------------------------------------------------------
                throw;
            }
        }
    }
}
