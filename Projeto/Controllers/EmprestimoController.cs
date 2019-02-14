using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Interfaces;
using Projeto.Models;

namespace Projeto.Controllers
{
    [Authorize]
    public class EmprestimoController : Controller
    {
        private readonly IAmigoBusiness _amigoBusiness;
        private readonly ITituloBusiness _tituloBusiness;

        public EmprestimoController(IAmigoBusiness amigoBusiness, ITituloBusiness tituloBusiness)
        {
            _amigoBusiness = amigoBusiness;
            _tituloBusiness = tituloBusiness;
        }

        public IActionResult Index()
        {
            return View(new EmprestimoViewModel
            {
                Amigos = _amigoBusiness.Consultar(User.Identity.Name),
                Titulos = _tituloBusiness.Consultar(User.Identity.Name)
            });
        }
    }
}
