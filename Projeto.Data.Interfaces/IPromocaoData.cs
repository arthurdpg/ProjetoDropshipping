using Projeto.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Projeto.Data.Interfaces
{
    public interface IPromocaoData
    {
        List<Promocao> Consultar();
        List<Promocao> ConsultarAtivas(DateTime data, int quantidade);
    }
}
