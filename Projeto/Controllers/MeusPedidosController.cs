using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.CrossCutting;

namespace Projeto.Controllers
{
    [Authorize(Roles = Perfis.Cliente)]
    public class MeusPedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}