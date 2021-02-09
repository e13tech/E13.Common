using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Data.Db
{
    public interface IUserAccessor
    {
        string CurrentUser { get; }
    }
}
