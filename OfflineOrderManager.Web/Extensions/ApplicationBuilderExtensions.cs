using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OfflineOrderManager.Data;

namespace OfflineOrderManager.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CreateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<OfflineOrderManagerDbContext>().Database.EnsureCreated();
            }

            return app;
        }
    }
}