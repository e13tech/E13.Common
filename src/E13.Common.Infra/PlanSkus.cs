using Pulumi.Azure.AppService.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Infra
{
    public static class PlanSkus
    {
        public static PlanSkuArgs AppService_Free() => new PlanSkuArgs
        {
            Tier = "Free",
            Size = "F1",
            Capacity = 0
        };
    }
}
