using Banco.Models;
using Microsoft.EntityFrameworkCore;

namespace Banco.Services
{
    public static class DbService
    {
        public static void InitialMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceDb = scope.ServiceProvider
                    .GetService<BancoDbContext>();

                serviceDb.Database.Migrate();
            }
        }
    }
}
