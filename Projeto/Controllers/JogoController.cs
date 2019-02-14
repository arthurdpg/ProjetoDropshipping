using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Interfaces;
using Projeto.Models.JogoViewModels;

namespace Projeto.Controllers
{
    [Authorize]
    public class JogoController : Controller
    {
        private readonly IConsoleBusiness _consoleBusiness;

        public JogoController(IConsoleBusiness consoleBusiness)
        {
            _consoleBusiness = consoleBusiness;
        }

        public IActionResult Index()
        {
            return View(new JogoViewModel
            {
                Consoles = _consoleBusiness.Listar()
            });
        }
    }
}