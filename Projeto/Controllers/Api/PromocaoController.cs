using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System.Collections.Generic;

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

        [HttpGet]
        public List<Promocao> Get()
        {
            //var jogos = _tituloBusiness.Consultar(User.Identity.Name);
            //var totalRegistros = jogos != null ? jogos.Count : 0;

            //return Json(new { recordsTotal = totalRegistros, data = jogos });
            return new List<Promocao>();
        }

        [HttpGet]
        public Promocao Get(int codigo)
        {
            //var jogos = _tituloBusiness.Consultar(User.Identity.Name);
            //var totalRegistros = jogos != null ? jogos.Count : 0;

            //return Json(new { recordsTotal = totalRegistros, data = jogos });
            return new Promocao();
        }
    }
}
