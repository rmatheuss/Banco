using Microsoft.EntityFrameworkCore;

namespace Banco.Infraestrutura
{
    public static class DBMigrations
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
