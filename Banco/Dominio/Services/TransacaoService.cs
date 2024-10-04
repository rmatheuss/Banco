using Banco.Dominio.Interfaces.Repositorios;
using Banco.Dominio.Interfaces.Services;
using Banco.Dominio.Entidades;
using iText.Html2pdf;
using System.Text;

namespace Banco.Dominio.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        public TransacaoService(ITransacaoRepositorio transacaoRepositorio) 
        {
            _transacaoRepositorio = transacaoRepositorio;
        }

        public async Task<Transacao> CriarTransacao(Transacao transacao)
            => await _transacaoRepositorio.CriarTransacao(transacao);

        public List<Transacao> BuscarTransacoesPorCorrentista(int idCorrentista, int quantidadeDias)
            => _transacaoRepositorio.BuscarTransacoesPorCorrentista(idCorrentista, quantidadeDias);

        public byte[] GerarExtratoEmPDFPorCorrentista(int idCorrentista, int quantidadeDias)
        {
            var extrato = _transacaoRepositorio.BuscarTransacoesPorCorrentista(idCorrentista, quantidadeDias);

            StringBuilder linhasTransacao = new StringBuilder();

            foreach (var item in extrato)
            {
                linhasTransacao.Append(@$"
                            <tr>
			                    <td>{item.Data.ToString("dd/MM")}</td>
			                    <td>{item.TipoTransacao}</td>
			                    <td>{item.Valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR"))}</td>
		                    </tr>");
            }

            string htmlContent = @$"
                <html>
                <head>
                    <title>Extrato</title>
                </head>
                <body>
                    <table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width:500px"">
	                    <tbody>
		                    <tr>
			                    <td>Data</td>
			                    <td>Tipo da transa&ccedil;&atilde;o</td>
			                    <td>Valor Monet&aacute;rio</td>
		                    </tr>
		                    {linhasTransacao}
	                    </tbody>
                    </table>
                </body>
                </html>";

            using (var stream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(htmlContent, stream);
                return stream.ToArray();
            }
        }
    }
}