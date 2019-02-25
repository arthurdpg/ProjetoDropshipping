using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.CrossCutting;

namespace Projeto.Controllers
{
    [Authorize(Roles = Perfis.Administrador)]
    public class PromocaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}