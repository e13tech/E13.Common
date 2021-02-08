using E13.Common.Core.Attributes;
using System;

namespace E13.Common.Infra
{
    public enum Environments
    {
        [Abbrevation("dev")]
        Development,

        [Abbrevation("int")]
        Integration,

        [Abbrevation("test")]
        Test,

        [Abbrevation("prod")]
        Production
    }
}
