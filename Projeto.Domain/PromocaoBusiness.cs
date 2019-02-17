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

        public List<Promocao> Consultar()
        {
            return _promocaoData.Consultar();
        }

        public List<Promocao> ConsultarAtivas(int quantidade)
        {
            return _promocaoData.ConsultarAtivas(DateTime.Now.Date, quantidade);
        }
    }
}
