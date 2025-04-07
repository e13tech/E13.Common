using E13.Common.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddStandardDbContext<TContext>(this IServiceCollection services, IConfiguration configuration, string configKey)
            where TContext : DbContext
        {
            if(EnvironmentVars.IsRunningInMemory())
            {
                services.AddDbContext<TContext>(options => 
                    options.UseInMemoryDatabase($"{typeof(TContext).Name}"));
            }
            else
            {
                var connection = configuration.GetConnectionString(configKey);
                services.AddDbContext<TContext>(options =>
                    options.UseSqlServer(connection, actions => 
                        actions.EnableRetryOnFailure()));
            }

            return services;
        }
        public static IServiceCollection AddStandardApi(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod());
            });
            services.AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            services.AddMvcCore()
                .AddApiExplorer();

            return services;
        }
    }
}
