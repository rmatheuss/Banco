using Banco.Dominio.Interfaces.Services;
using Banco.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : Controller
    {
        private readonly ITransacaoService _transacaoService;
        public TransacoesController(ITransacaoService transacaoService) 
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _transacaoService.CriarTransacao(transacao);
                    return Created();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }             
            }

            return BadRequest(transacao);
        }

        [HttpGet("extrato")]
        public IActionResult BuscarExtrato([FromQuery] int idCorrentista, [FromQuery] int quantidadeDias = 5)
        {
            return Ok(_transacaoService.BuscarTransacoesPorCorrentista(idCorrentista, quantidadeDias));
        }

        [HttpGet("extrato/pdf")]
        public IActionResult BuscarExtratoEmPdf([FromQuery] int idCorrentista, [FromQuery] int quantidadeDias = 5)
        {
            var pdfBytes = _transacaoService.GerarExtratoEmPDFPorCorrentista(idCorrentista, quantidadeDias);
            var fileName = $"extrato_{idCorrentista}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}