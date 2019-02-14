using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.CrossCutting;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Jogo")]
    [Authorize]
    public class JogoController : Controller
    {
        private readonly ITituloBusiness _tituloBusiness;

        public JogoController(ITituloBusiness tituloBusiness)
        {
            _tituloBusiness = tituloBusiness;
        }

        [HttpGet(Name = "GetJogos")]
        public JsonResult GetJogos()
        {
            var jogos = _tituloBusiness.Consultar(User.Identity.Name);
            var totalRegistros = jogos != null ? jogos.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = jogos });
        }

        [HttpGet("{codigo}", Name = "GetJogo")]
        public Titulo GetJogo(int codigo)
        {
            return _tituloBusiness.Obter(codigo);
        }

        [HttpPost(Name = "PostJogo")]
        public JsonResult PostJogo(Titulo jogo)
        {
            if (jogo != null)
            {
                jogo.Usuario = User.Identity.Name;
                try
                {
                    _tituloBusiness.Salvar(jogo);
                    return Json(new { operacaoConcluidaComSucesso = true });
                }
                catch (ProjetoException pe)
                {
                    return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
                }
            }
            return Json(new { operacaoConcluidaComSucesso = false });
        }

        [HttpDelete("{codigo}", Name = "DeleteJogo")]
        public JsonResult DeleteJogo(int codigo)
        {
            try
            {
                _tituloBusiness.Excluir(codigo);
                return Json(new { operacaoConcluidaComSucesso = true });
            }
            catch (ProjetoException pe)
            {
                return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
            }
        }
    }
}
