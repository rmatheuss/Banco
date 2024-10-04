using Banco.Dominio.Entidades;

namespace Banco.Dominio.Interfaces.Repositorios
{
    public interface ITransacaoRepositorio
    {
        Task<Transacao> CriarTransacao(Transacao transacao);

        List<Transacao> BuscarTransacoesPorCorrentista(int idCorrentista, int quantidadeDias);
    }
}