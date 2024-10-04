using Banco.Dominio.Entidades;

namespace Banco.Dominio.Interfaces.Services
{
    public interface ITransacaoService
    {
        Task<Transacao> CriarTransacao(Transacao transacao);

        List<Transacao> BuscarTransacoesPorCorrentista(int idCorrentista, int quantidadeDias);

        byte[] GerarExtratoEmPDFPorCorrentista(int idCorrentista, int quantidadeDias);
    }
}