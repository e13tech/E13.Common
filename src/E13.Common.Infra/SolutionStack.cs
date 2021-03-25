using Pulumi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E13.Common.Infra;
using Pulumi.Azure.Core;
using Pulumi.Azure.AppInsights;
using Pulumi.AzureAD;
using Pulumi.Azure.AppService;
using Pulumi.Azure.Sql;
using Pulumi.Azure.Maintenance;

namespace E13.Common.Infra
{
    public class SolutionStack : Stack
    {
        public static readonly string DefaultPassword = "P@ssword1";

        public string Prefix;

        public Output<string> RGName 
        { 
            get
            {
                if(_rgName == null)
                {
                    var rg = new ResourceGroup($"{Prefix}-rg", new ResourceGroupArgs
                    {
                        Tags = StandardTags(),
                        Location = "centralus"
                    });
                    _rgName = rg.Name;
                }
                return _rgName;
            }
        }
        private Output<string>? _rgName;

        public Output<string> AppInsightsKey
        {
            get
            {
                if(_appInsightsKey == null)
                {
                    var i = new Insights($"ai", new InsightsArgs
                    {
                        //ApplicationType = "web",
                        ResourceGroupName = RGName.Apply(n => n),

                        Tags = StandardTags()
                    }); ;
                    _appInsightsKey = i.InstrumentationKey;
                }
                return _appInsightsKey;
            }
        }
        private Output<string>? _appInsightsKey;

        public Output<string>? _sqlServerName;
        public Output<string> SqlServerName 
        { 
            get 
            {
                if(_sqlServerName == null)
                {
                    var sql = new SqlServer($"{Prefix}-sql", new SqlServerArgs
                    {
                        ResourceGroupName = RGName,
                        AdministratorLogin = User,
                        AdministratorLoginPassword = Password,
                        Tags = StandardTags(),
                        Version = "12.0"
                    });

                    _sqlServerName = sql.Name;
                }

                return _sqlServerName;
            }
        }

        public Output<string>? _appServicePlanId;
        public Output<string> AppServicePlanId
        {
            get
            {
                if(_appServicePlanId == null)
                {
                    var plan = new Plan($"{Prefix}plan", new PlanArgs
                    {
                        ResourceGroupName = RGName,
                        Kind = "app",
                        Sku = PlanSkus.AppService_Free(),
                        PerSiteScaling = false,
                        MaximumElasticWorkerCount = 1,
                        IsXenon = false,
                        Reserved = false,
                        Tags = StandardTags()
                    });
                    _appServicePlanId = plan.Id;
                }
                return _appServicePlanId;
            }
        }

        private string User => Configuration.Get("dbadmin") ?? "e13";
        private string Password => Configuration.Get("dbpass") ?? "P@ssw0rd!";

        public Environments TargetEnvironment => Configuration.Require("env") switch
        {
            "prod" => Environments.Production,
            "auto" => Environments.Automation,
            "stg" => Environments.Staging,
            _ => Environments.Development,
        };

        public readonly Pulumi.Config Configuration = new();

        public SolutionStack()
        {
            Prefix = $"{GetResourceName()}-";
        }

        public Output<string> ConnectionStringFor(Output<string> database)
            => Output.Format($"Server= tcp:{SqlServerName.Apply(s => s)}.database.windows.net;initial catalog={database.Apply(d => d)};userID={User};password={Password};Min Pool Size=0;Max Pool Size=30;Persist Security Info=true;");

        public InputMap<string> StandardTags()
            => new()
            {
                { "Environment", TargetEnvironment.ToString() }
            };
    }
}
