using E13.Common.Infra;
using Pulumi;
using Pulumi.Azure.AppInsights;
using Pulumi.Azure.AppService;
using Pulumi.Azure.Storage.Inputs;
using Pulumi.AzureAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureAD = Pulumi.AzureAD;

namespace E13.Common.Infra
{
    public static class StaticServiceExtensions
    {
        public static StaticService DefineStaticService(this SolutionStack stack, string serviceName)
            => new StaticService(stack, serviceName);
    }
    public class StaticService
    {
        public Output<string> StaticEndpoint { get; set; }

        public StaticService(SolutionStack rg, string serviceName)
        {
            var sa = new Pulumi.Azure.Storage.Account(serviceName, new Pulumi.Azure.Storage.AccountArgs
            {
                ResourceGroupName = rg.RGName,
                EnableHttpsTrafficOnly = true,
                AccountReplicationType = "LRS",
                AccountTier = "Standard",
                AccountKind = "StorageV2",
                StaticWebsite = new AccountStaticWebsiteArgs
                {
                    IndexDocument = "index.html"
                }
            });

            StaticEndpoint = sa.PrimaryWebEndpoint;
        }
    }
}
