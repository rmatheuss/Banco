using Banco.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infraestrutura
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

        public DbSet<Transacao>? Transacoes { get; set; }
    }
}
