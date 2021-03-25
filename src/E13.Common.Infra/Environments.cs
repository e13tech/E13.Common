using E13.Common.Core.Attributes;
using System;

namespace E13.Common.Infra
{
    public enum Environments
    {
        [Abbrevation("dev")]
        Development,

        [Abbrevation("stg")]
        Staging,

        [Abbrevation("auto")]
        Automation,

        [Abbrevation("prod")]
        Production
    }
}
