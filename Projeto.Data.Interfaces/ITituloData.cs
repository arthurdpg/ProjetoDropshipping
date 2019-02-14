using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Data.Interfaces
{
    public interface ITituloData
    {
        Titulo Obter(int codigo);
        List<Titulo> Consultar(string usuario);
        void Excluir(int codigo);
        void Salvar(Titulo amigo);
    }
}
