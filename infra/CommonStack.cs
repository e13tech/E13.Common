using System;
using System.Threading.Tasks;
using E13.Common.Infra;
using Pulumi;
using Pulumi.AzureAD;
using Pulumi.AzureAD.Inputs;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;

class CommonStack : SolutionStack
{
    public CommonStack()
    {
        RG = RGName;

        var validDomain = Configuration.Require("autodomain");
        AutoUserPasswords = Output.Create(Configuration.Get("autopass") ?? DefaultPassword);
        AutoEnabledUser = Output.Create($"auto.enabled@{validDomain}");
        AutoDisabledUser = Output.Create($"auto.disabled@{validDomain}");

        var autoEnabled = new User("auto.enabled", new UserArgs
        {
            DisplayName = "auto.enabled",
            Password = AutoUserPasswords.Apply(p => p),
            UserPrincipalName = AutoEnabledUser.Apply(p => p),
            AccountEnabled = true
        });
        var autoDisabled = new User("auto.disabled", new UserArgs
        {
            DisplayName = "auto.disabled",
            Password = AutoUserPasswords.Apply(p => p),
            UserPrincipalName = AutoDisabledUser.Apply(p => p),
            AccountEnabled = false
        });
        var appReg = new Application("auto.registration", new ApplicationArgs
        {
            PublicClient = true,
            Oauth2AllowImplicitFlow = true,
            AvailableToOtherTenants = true,
            DisplayName = "auto.registration",
            RequiredResourceAccesses =
            {
                new ApplicationRequiredResourceAccessArgs
                {
                    ResourceAppId = "00000003-0000-0000-c000-000000000000", // MS Graph
                    ResourceAccesses =
                    {
                        new ApplicationRequiredResourceAccessResourceAccessArgs
                        {  
                            Id = "a154be20-db9c-4678-8ab7-66f6cc099a59", // user.read.all
                            Type = "Scope",
                        }
                    }
                }
            }
        });
        // az ad app permission admin-consent --id 796fdf79-be86-4554-b34c-871f95cf3293
        TestClientId = appReg.ApplicationId;
        //// Create an Azure Resource Group
        //var resourceGroup = new ResourceGroup("e13-rg-github");

        //// Create an Azure resource (Storage Account)
        //var storageAccount = new StorageAccount("sa", new StorageAccountArgs
        //{
        //    ResourceGroupName = resourceGroup.Name,
        //    Sku = new SkuArgs
        //    {
        //        Name = SkuName.Standard_LRS
        //    },
        //    Kind = Kind.StorageV2
        //});

        //// Export the primary key of the Storage Account
        //this.PrimaryStorageKey = Output.Tuple(resourceGroup.Name, storageAccount.Name).Apply(names =>
        //    Output.CreateSecret(GetStorageAccountPrimaryKey(names.Item1, names.Item2)));
    }

    [Output]
    public Output<string> AutoUserPasswords { get; set; }

    [Output]
    public Output<string> AutoEnabledUser { get; set; }

    [Output]
    public Output<string> AutoDisabledUser { get; set; }
    [Output]
    public Output<string> TestClientId { get; set; }
    [Output]
    public Output<string> RG { get; set; }
    //[Output]
    //public Output<string> PrimaryStorageKey { get; set; }

    //private static async Task<string> GetStorageAccountPrimaryKey(string resourceGroupName, string accountName)
    //{
    //    var accountKeys = await ListStorageAccountKeys.InvokeAsync(new ListStorageAccountKeysArgs
    //    {
    //        ResourceGroupName = resourceGroupName,
    //        AccountName = accountName
    //    });
    //    return accountKeys.Keys[0].Value;
    //}
}
