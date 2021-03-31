using FluentAssertions;
using Microsoft.Identity.Client;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace E13.Common.Nunit.Api.TestsInt
{
    public class HttpClientExtensionTests : LaunchSettingsFixture
    {
        /*
         * https://docs.microsoft.com/en-us/office365/admin/add-users/set-password-to-never-expire?view=o365-worldwide
         * Set-AzureADUser -ObjectId <user ID> -PasswordPolicies DisablePasswordExpiration
         */

        private static readonly string TestGoodUserName = Environment.GetEnvironmentVariable("TokenForAAD_TestUser");
        private static readonly string TestDisabledUserName = Environment.GetEnvironmentVariable("TokenForAAD_DisabledUser");
        private static readonly string TestInvalidUserName = Environment.GetEnvironmentVariable("TokenForAAD_InvalidUser");

        private static readonly string TestPassword = Environment.GetEnvironmentVariable("TokenForAAD_TestPass");

        [Test]
        public void Test_Env()
        {
            TestGoodUserName.Should().Be("unknown");
        }

        [Test]
        public void TokenForAAD_ValidCredential_GetsBearerToken()
        {
            using var client = new HttpClient();

            client.TokenForAAD(TestGoodUserName, TestPassword.Secure());

            client.DefaultRequestHeaders.Authorization.Scheme.Should().Be("Bearer");
            client.DefaultRequestHeaders.Authorization.Parameter.Should().NotBeNullOrWhiteSpace();
        }
        [Test]
        public void TokenForAAD_DisabledUser_Unauthorized()
        {
            using var client = new HttpClient();

            Action a = () => client.TokenForAAD(TestDisabledUserName, TestPassword.Secure());

            a.Should().Throw<AggregateException>().WithInnerException<MsalUiRequiredException>()
                .WithMessage("AADSTS50057: *").And
                .ErrorCode.Should().Be("invalid_grant");
        }
        [Test]
        public void TokenForAAD_InvalidUser_Unauthorized()
        {
            using var client = new HttpClient();

            Action a = () => client.TokenForAAD(TestInvalidUserName, "junk".Secure());

            a.Should().Throw<AggregateException>().WithInnerException<MsalUiRequiredException>()
                .WithMessage("AADSTS50034: *").And
                .ErrorCode.Should().Be("invalid_grant");
        }
        [Test]
        public void TokenForAAD_BadData_Exception()
        {
            using var client = new HttpClient();

            Action a = () => client.TokenForAAD("junk", "junk".Secure());

            a.Should().Throw<AggregateException>().WithInnerException<MsalClientException>()
                .WithMessage("Unsupported User Type 'Unknown'*").And
                .ErrorCode.Should().Be("unknown_user_type");
        }
    }
}
