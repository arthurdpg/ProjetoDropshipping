using Microsoft.EntityFrameworkCore;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Data
{
    public class PromocaoData : CrudData<Promocao>, IPromocaoData
    {
        public PromocaoData(ProjetoContext context) : base(context)
        {

        }

        public List<Promocao> Consultar()
        {
            return Context.Promocoes
                .Include(p => p.Produto)
                .OrderByDescending(p => p.DataInicio)
                .ToList();
        }

        public List<Promocao> ConsultarAtivas(DateTime data, int quantidade)
        {
            return Context.Promocoes
                .Include(p => p.Produto)
                .OrderByDescending(p => p.DataInicio)
                .Where(p => p.DataInicio <= data && (p.DataFim == null || p.DataFim >= data))
                .Take(quantidade)
                .ToList();
        }
    }
}
