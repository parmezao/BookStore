using Book_Store.Data;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services
{
    public static class DatabaseManagementService
    {
        public static void AddMigrationInitialization(this IServiceCollection services, 
            IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {                
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();
            }
        }
    }
}