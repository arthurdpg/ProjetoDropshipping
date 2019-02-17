using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface IClienteBusiness
    {
        ResultadoDto ValidarCadastroCliente(Cliente novoCliente);
        Cliente ObterPorLogin(string login);
        Cliente ObterPorCpf(string cpf);
        ResultadoDto Salvar(Cliente cliente);
    }
}
