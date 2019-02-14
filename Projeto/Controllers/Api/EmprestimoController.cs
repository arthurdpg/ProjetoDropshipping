using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.CrossCutting;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Emprestimo")]
    [Authorize]
    public class EmprestimoController : Controller
    {
        private readonly IEmprestimoBusiness _emprestimoBusiness;

        public EmprestimoController(IEmprestimoBusiness emprestimoBusiness)
        {
            _emprestimoBusiness = emprestimoBusiness;
        }

        [HttpGet(Name = "GetEmprestimosEmAndamento")]
        [Route("GetEmprestimosEmAndamento")]
        public JsonResult GetEmprestimosEmAndamento()
        {
            var emprestimos = _emprestimoBusiness.ConsultarEmAndamento(User.Identity.Name);
            var totalRegistros = emprestimos != null ? emprestimos.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = emprestimos });
        }

        [HttpGet(Name = "GetEmprestimosFinalizados")]
        [Route("GetEmprestimosFinalizados")]
        public JsonResult GetEmprestimosFinalizados()
        {
            var emprestimos = _emprestimoBusiness.ConsultarFinalizados(User.Identity.Name);
            var totalRegistros = emprestimos != null ? emprestimos.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = emprestimos });
        }

        [HttpGet("{codigo}", Name = "GetEmprestimo")]
        public Emprestimo GetEmprestimo(int codigo)
        {
            return _emprestimoBusiness.Obter(codigo);
        }

        [HttpPost(Name = "PostSalvarEmprestimo")]
        [Route("SalvarEmprestimo")]
        public JsonResult PostSalvarEmprestimo(Emprestimo emprestimo)
        {
            if (emprestimo != null)
            {
                emprestimo.Usuario = User.Identity.Name;
                
                try
                {
                    _emprestimoBusiness.Salvar(emprestimo);
                    return Json(new { operacaoConcluidaComSucesso = true });
                }
                catch (ProjetoException pe)
                {
                    return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
                }
            }
            return Json(new { operacaoConcluidaComSucesso = false });
        }

        [HttpPost(Name = "PostFinalizarEmprestimo")]
        [Route("FinalizarEmprestimo")]
        public JsonResult PostFinalizarEmprestimo(int codigo)
        {
            try
            {
                _emprestimoBusiness.Finalizar(codigo);
                return Json(new { operacaoConcluidaComSucesso = true });
            }
            catch (ProjetoException pe)
            {
                return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
            }
        }

        [HttpDelete("{codigo}", Name = "DeleteEmprestimo")]
        public void DeleteEmprestimo(int codigo)
        {
            _emprestimoBusiness.Excluir(codigo);
        }
    }
}
