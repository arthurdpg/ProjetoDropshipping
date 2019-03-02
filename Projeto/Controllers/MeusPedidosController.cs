using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    public class MeusPedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}