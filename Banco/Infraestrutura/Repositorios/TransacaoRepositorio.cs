using Banco.Dominio.Interfaces.Repositorios;
using Banco.Dominio.Entidades;

namespace Banco.Infraestrutura.Repositorios
{
    public class TransacaoRepositorio : ITransacaoRepositorio
    {
        private readonly BancoDbContext _dbContext;
        public TransacaoRepositorio(BancoDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Transacao> CriarTransacao(Transacao transacao)
        {
            transacao.Data = DateTime.Now;
            var novaTransacao = await _dbContext.Transacoes.AddAsync(transacao);
            await _dbContext.SaveChangesAsync();
            return novaTransacao.Entity;
        }

        public List<Transacao> BuscarTransacoesPorCorrentista(int idCorrentista, int quantidadeDias)
        {
            DateTime dataLimite = DateTime.Now.AddDays(-quantidadeDias).Date;
            var extrato =
                _dbContext.Transacoes.Where(w => w.IdCorrentista == idCorrentista && w.Data >= dataLimite).ToList();
            return extrato;
        }
    }
}