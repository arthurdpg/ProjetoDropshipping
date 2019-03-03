using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Projeto.Domain
{
    public class PromocaoBusiness : IPromocaoBusiness
    {
        private readonly IPromocaoData _promocaoData;

        public PromocaoBusiness(IPromocaoData promocaoData)
        {
            _promocaoData = promocaoData;
        }

        public Promocao Obter(int codigo)
        {
            return _promocaoData.Obter(codigo);
        }

        public List<Promocao> Consultar()
        {
            return _promocaoData.Consultar();
        }

        public List<Promocao> ConsultarAtivas(int quantidade)
        {
            return _promocaoData.ConsultarAtivas(DateTime.Now, quantidade);
        }

        public ResultadoDto Finalizar(int codigo)
        {
            try
            {
                var promocao = Obter(codigo);

                if (promocao == null)
                    return new ResultadoDto(false, Mensagens.MensagemOperacaoRegistroNaoEncontrado);

                promocao.DataFim = DateTime.Now;

                _promocaoData.Salvar(promocao);
                return new ResultadoDto(true, Mensagens.MensagemOperacaoSucesso);
            }
            catch (Exception)
            {
                return new ResultadoDto(false, Mensagens.MensagemOperacaoErro);
            }
        }
    }
}
