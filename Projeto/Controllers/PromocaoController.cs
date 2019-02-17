using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    public class PromocaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}