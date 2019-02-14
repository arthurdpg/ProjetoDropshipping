﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;

namespace Projeto.Data
{
    public class TituloData : CrudData<Titulo>, ITituloData
    {
        public TituloData(ProjetoContext context) : base(context)
        {
        }

        public override Titulo Obter(int codigo)
        {
            return Context.Titulos
                .Include(t => t.Console)
                .FirstOrDefault(t => t.Codigo == codigo);
        }

        public List<Titulo> Consultar(string usuario)
        {
            return Context.Titulos
                .Include(t => t.Console)
                .Where(t => t.Usuario == usuario).ToList();
        }
    }
}
