using Microsoft.EntityFrameworkCore;
using System;

namespace E13.Common.Data.Db
{
    public interface IDataSeed<TContext> where TContext : DbContext
    {
        void Seed(TContext context);
    }
}
