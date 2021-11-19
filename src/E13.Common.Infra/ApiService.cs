using E13.Common.Infra;
using Pulumi;
using Pulumi.Azure.AppInsights;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.Core;
using Pulumi.Azure.Network;
using Pulumi.Azure.Sql;
using Pulumi.Azure.Storage.Inputs;
using Pulumi.AzureAD;
using Pulumi.AzureNative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureAD = Pulumi.AzureAD;

namespace E13.Common.Infra
{
    public static class ApiServiceExtensions
    {
        public static ApiService DefineApiService(this SolutionStack stack, string serviceName, Persistence includedPersistence, bool registerSwagger = true)
            => new(stack, serviceName, includedPersistence, registerSwagger);
    }

    public class ApiService
    {
        public Output<string> ApiClientId { get; set; }
        public Output<string> ApiUri { get; set; }

        private readonly Output<string>? _apiSwaggerClientId;
        public Output<string> ApiSwaggerClientId
        {
            get
            {
                if (_apiSwaggerClientId == null)
                    throw new NotImplementedException("Swagger was not registered for authentication");

                return _apiSwaggerClientId;
            }
        }
        public Output<string>? DbConnectionString;

        private readonly string _name;
        private readonly Output<string>? _database;

        public ApiService(SolutionStack stack, string serviceName, Persistence included, bool registerSwagger = true)
        {
            _name = $"{stack.Prefix}{serviceName}";

            if (included.HasFlag(Persistence.AzureSql))
            {
                var db = new Database($"{serviceName}", new DatabaseArgs
                {
                    ResourceGroupName = stack.RGName,
                    ServerName = stack.SqlServerName,
                    RequestedServiceObjectiveName = "S0"
                });

                _database = db.Name;
                DbConnectionString = stack.ConnectionStringFor(_database);
            }

            var a = new AppService(_name, new AppServiceArgs
            {
                ResourceGroupName = stack.RGName,
                AppServicePlanId = stack.AppServicePlanId,
                Tags = stack.StandardTags(),
                AppSettings =
                {
                    {"APPLICATIONINSIGHTS_CONNECTION_STRING", Output.Format($"InstrumentationKey={stack.AppInsightsKey}")}
                },
                ConnectionStrings = GenerateConnStrings(included)
            });

            var r = AppRegistration(a);

            ApiClientId = r.ApplicationId;
            ApiUri = r.IdentifierUris.First();

            if (registerSwagger)
            {
                var s = SwaggerRegistration(a, r);
                _apiSwaggerClientId = s.ApplicationId;
            }
        }

        private InputList<AppServiceConnectionStringArgs> GenerateConnStrings(Persistence includedPersistence)
        {
            var connStrings = new InputList<AppServiceConnectionStringArgs>();
            if (includedPersistence.HasFlag(Persistence.AzureSql))
            {
                connStrings.Add(new AppServiceConnectionStringArgs
                {
                    Name = "db",
                    Type = "SQLAzure",
#pragma warning disable CS8604 // Possible null reference argument. Cannot happen based on code flow
                    Value = DbConnectionString
#pragma warning restore CS8604 // Possible null reference argument.
                });
            }
            return connStrings;
        }

        private Application AppRegistration(AppService a)
        {
            var registration = new Application(_name, new ApplicationArgs
            {
                //AvailableToOtherTenants = false,
                //Homepage = a.DefaultSiteHostname.Apply(host => $"https://{host}"),
                IdentifierUris =
                {
                    a.Name.Apply(n => $"api://{n}"),
                },
                //Oauth2AllowImplicitFlow = true,
                // user_impresonation is automagically created so does not need to be explicitly defined
                //Oauth2Permissions =
                //{
                //    new Pulumi.AzureAD.Inputs.ApplicationOauth2PermissionArgs
                //    {
                //        AdminConsentDescription = "Allow the application to access example on behalf of the signed-in user.",
                //        AdminConsentDisplayName = "Access example",
                //        IsEnabled = true,
                //        Type = "User",
                //        UserConsentDescription = "Allow the application to access example on your behalf.",
                //        UserConsentDisplayName = "Access example",
                //        Value = "user_impersonation",
                //    },
                //},
                Owners =
                {
                    "74c90e60-fe2b-4735-84cd-476a1c9181fb", // jj.bussert@gmail.com in e13tech
                },
                //ReplyUrls =
                //{
                //    //a.DefaultSiteHostname.Apply(host => $"https://{host}/swagger/oauth2-redirect.html"),
                //},
                RequiredResourceAccesses =
                {
                    //new Pulumi.AzureAD.Inputs.ApplicationRequiredResourceAccessArgs
                    //{
                    //    ResourceAccesses =
                    //    {
                    //        new Pulumi.AzureAD.Inputs.ApplicationRequiredResourceAccessResourceAccessArgs
                    //        {
                    //            Id = "702d514b-f802-47ac-ac3c-c4b7a4010e9e",
                    //            Type = "Scope",
                    //        },
                    //    },
                    //    ResourceAppId = "c05e5f6b-434f-43a3-b043-a957047bf13d",
                    //},
                },
                //Type = "webapp/api",
            });

            var sp = new ServicePrincipal(_name, new ServicePrincipalArgs
            {
                ApplicationId = registration.ApplicationId.Apply(i => i)
            });

            return registration;
        }

        private Application SwaggerRegistration(AppService a, Application app)
        {
            var swaggerName = $"{_name}-swagger";
            var registration = new Application(swaggerName, new ApplicationArgs
            {
                //AvailableToOtherTenants = false,
                //Oauth2AllowImplicitFlow = true,
                //Homepage = a.DefaultSiteHostname.Apply(host => $"https://{host}"),
                Owners =
                {
                    "74c90e60-fe2b-4735-84cd-476a1c9181fb", // jj.bussert@gmail.com in e13tech
                },
                //ReplyUrls =
                //{
                //    a.DefaultSiteHostname.Apply(host => $"https://{host}/swagger/oauth2-redirect.html"),
                //    "https://localhost:5001/swagger/oauth2-redirect.html"
                //},
                RequiredResourceAccesses =
                {
                    new Pulumi.AzureAD.Inputs.ApplicationRequiredResourceAccessArgs
                    {
                        ResourceAccesses =
                        {
                            new Pulumi.AzureAD.Inputs.ApplicationRequiredResourceAccessResourceAccessArgs
                            {
                                Id = app.Oauth2PermissionScopeIds.Apply(p => p.First(x => x.Value == "user_impersonation").Key),
                                Type = "Scope",
                            },
                        },
                        ResourceAppId = app.ApplicationId.Apply(id => id),
                    },
                },
                //Type = "webapp/api", TODO fix type?
            })
            {

            };
            var sp = new ServicePrincipal(swaggerName, new ServicePrincipalArgs
            {
                ApplicationId = registration.ApplicationId.Apply(i => i)
            });

            return registration;
        }

    }
}
