using Microsoft.EntityFrameworkCore;

namespace Banco.Models
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

        public DbSet<Transacao>? Transacoes { get; set; }
    }
}
