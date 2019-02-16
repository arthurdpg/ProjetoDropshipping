using Projeto.Domain.Entities;

namespace Projeto.Data.Interfaces
{
    public interface IClienteData
    {
        Cliente ObterPorLogin(string login);
        Cliente ObterPorCpf(string cpf);
        void Salvar(Cliente cliente);
    }
}
