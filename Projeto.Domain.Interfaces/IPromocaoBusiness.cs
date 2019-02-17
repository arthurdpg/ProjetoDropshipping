using Projeto.Domain.Entities;
using System.Collections.Generic;

namespace Projeto.Domain.Interfaces
{
    public interface IPromocaoBusiness
    {
        List<Promocao> Consultar();
        List<Promocao> ConsultarAtivas(int quantidade);
    }
}
