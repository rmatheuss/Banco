using Banco.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Controllers
{
    public class TransacoesController : Controller
    {
        private readonly BancoDbContext _dbContext;
        public TransacoesController(BancoDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        [HttpGet]
        public IActionResult BuscarExtrato()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BuscarExtratoEmPdf()
        {
            return View();
        }
    }
}
