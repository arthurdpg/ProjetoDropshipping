using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Interfaces;

namespace Projeto.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Promocao")]
    public class PromocaoController : Controller
    {
        private readonly IPromocaoBusiness _promocaoBusiness;
        
        public PromocaoController(IPromocaoBusiness promocaoBusiness)
        {
            _promocaoBusiness = promocaoBusiness;
        }

        [HttpGet(Name = "GetPromocoes")]
        public JsonResult Get()
        {
            var promocoes = _promocaoBusiness.Consultar();
            var totalRegistros = promocoes != null ? promocoes.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = promocoes });
        }
    }
}
