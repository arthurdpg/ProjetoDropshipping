using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.CrossCutting;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Amigo")]
    [Authorize]
    public class AmigoController : Controller
    {
        private readonly IAmigoBusiness _amigoBusiness;

        public AmigoController(IAmigoBusiness amigoBusiness)
        {
            _amigoBusiness = amigoBusiness;
        }

        [HttpGet(Name = "GetAmigos")]
        public JsonResult GetAmigos()
        {
            var amigos = _amigoBusiness.Consultar(User.Identity.Name);
            var totalRegistros = amigos != null ? amigos.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = amigos });
        }

        [HttpGet("{codigo}", Name = "GetAmigo")]
        public Amigo GetAmigo(int codigo)
        {
            return _amigoBusiness.Obter(codigo);
        }

        [HttpPost(Name= "PostAmigo")]
        public JsonResult PostAmigo(Amigo amigo)
        {
            if (amigo != null)
            {
                amigo.Usuario = User.Identity.Name;
                try
                {
                    _amigoBusiness.Salvar(amigo);
                    return Json(new { operacaoConcluidaComSucesso = true });
                }
                catch (ProjetoException pe)
                {
                    return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
                }
            }
            return Json(new { operacaoConcluidaComSucesso = false });
        }

        [HttpDelete("{codigo}", Name = "DeleteAmigo")]
        public JsonResult DeleteAmigo(int codigo)
        {
            try
            {
                _amigoBusiness.Excluir(codigo);
                return Json(new { operacaoConcluidaComSucesso = true });
            }
            catch (ProjetoException pe)
            {
                return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
            }
        }
    }
}
