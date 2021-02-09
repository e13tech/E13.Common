
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace E13.Common.Api
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder InitializeDbContext<TContext>(this IApplicationBuilder app, IConfiguration config)//, IHostEnvironment env)//, IDataSeed<TContext> dataSeed = null)
            where TContext : DbContext
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (config.RunningInMemory())
            {
                using var scope = app.ApplicationServices.CreateScope();
                using var ctx = scope.ServiceProvider.GetService<TContext>();

                ctx.Database.EnsureCreated();

                //if (dataSeed != null)
                //    using (var scope = app.ApplicationServices.CreateScope())
                //    using (var ctx = scope.ServiceProvider.GetService<TContext>())
                //        dataSeed.Seed(ctx);
            }
            return app;
        }
        public static IApplicationBuilder UseStandardApi(this IApplicationBuilder app, IConfiguration configuration, string apiTitle, string apiVersion)
        {
            if(!configuration.RunningInMemory())
                app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCors("AllowAllOrigins");

            // UseRouting must precede the UseSwagger lines so that it takes precidence when a routing conflict occurs
            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{apiVersion}/swagger.json", apiTitle);
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
