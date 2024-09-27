using Banco.Models;
using Microsoft.AspNetCore.Mvc;
using iText.Html2pdf;
using System.Text;

namespace Banco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : Controller
    {
        private readonly BancoDbContext _dbContext;

        public TransacoesController(BancoDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    transacao.Data = DateTime.Now;
                    _dbContext.Transacoes.Add(transacao);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ERRO02", $"ERRO {ex.Message}");
                    throw;
                }             
            }

            ModelState.AddModelError("ERRO01", "Modelo inválido!");
            return View(transacao);
        }

        [HttpGet("extrato")]
        public IActionResult BuscarExtrato([FromQuery] int idCorrentista, [FromQuery] int quantidadeDias = 5)
        {
            DateTime dataLimite = DateTime.Now.AddDays(-quantidadeDias).Date;
            var extrato = 
                _dbContext.Transacoes.Where(w => w.IdCorrentista == idCorrentista && w.Data >= dataLimite).ToList();

            return Ok(extrato);
        }

        [HttpGet("extrato/pdf")]
        public IActionResult BuscarExtratoEmPdf([FromQuery] int idCorrentista, [FromQuery] int quantidadeDias = 5)
        {
            DateTime dataLimite = DateTime.Now.AddDays(-quantidadeDias).Date;
            var extrato =
                _dbContext.Transacoes.Where(w => w.IdCorrentista == idCorrentista && w.Data >= dataLimite).ToList();

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
                var fileName = "extrato.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}
