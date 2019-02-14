using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [Authorize]
    public class AmigoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}