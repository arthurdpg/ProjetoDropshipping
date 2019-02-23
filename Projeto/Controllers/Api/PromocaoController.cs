using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Projeto.Configuration;
using Projeto.CrossCutting;
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
        private readonly IOptions<FilesConfig> _filesConfig;

        public PromocaoController(IPromocaoBusiness promocaoBusiness, IOptions<FilesConfig> filesConfig)
        {
            _promocaoBusiness = promocaoBusiness;
            _filesConfig = filesConfig;
        }

        [HttpGet(Name = "GetPromocoes")]
        public JsonResult Get()
        {
            var promocoes =  ConverterResultado(_promocaoBusiness.Consultar());
            var totalRegistros = promocoes != null ? promocoes.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = promocoes });
        }

        [HttpPut("{codigo}", Name = "Finalizar")]
        public JsonResult Finalizar(int codigo)
        {
            var resultado = _promocaoBusiness.Finalizar(codigo);
            return Json(new { operacaoConcluidaComSucesso = resultado.Sucesso, mensagem = resultado.Mensagem });
        }

        private List<PromocaoDto> ConverterResultado(List<Promocao> promocoes)
        {
            if (promocoes == null)
                return null;

            var resultado = new List<PromocaoDto>();
            promocoes.ForEach(p =>
            {
                resultado.Add(new PromocaoDto
                {
                    Codigo = p.Codigo,
                    NomeProduto = p.Produto.Nome,
                    DataInicio = p.DataInicio.ToString(Formatos.FormatoDataPtBr),
                    DataFim = p.DataFim.HasValue ? p.DataFim.Value.ToString(Formatos.FormatoDataPtBr) : string.Empty,
                    Finalizada = p.DataFim.HasValue,
                    CaminhoImagemProduto = ImagemHelper.GetImagemProduto(_filesConfig.Value, p.Produto.Codigo)
                });
            });

            return resultado;
        }
    }
}
