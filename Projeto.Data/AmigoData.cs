﻿using System.Collections.Generic;
using System.Linq;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;

namespace Projeto.Data
{
    public class AmigoData : CrudData<Amigo>, IAmigoData
    {
        public AmigoData(ProjetoContext context) : base(context)
        {
        }

        public List<Amigo> Consultar(string usuario)
        {
            return Context.Amigos.Where(a => a.Usuario == usuario).ToList();
        }
    }
}
