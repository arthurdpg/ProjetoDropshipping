using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly IClienteData _clienteData;

        public ClienteBusiness(IClienteData clienteData)
        {
            _clienteData = clienteData;
        }

        public Cliente ObterPorLogin(string login)
        {
            return _clienteData.ObterPorLogin(login);
        }

        public Cliente ObterPorCpf(string cpf)
        {
            return _clienteData.ObterPorCpf(cpf);
        }

        public void Salvar(Cliente cliente)
        {
            throw new System.NotImplementedException();
        }

        public ResultadoDto ValidarCadastroCliente(Cliente novoCliente)
        {
            var valido = novoCliente.Validar();

            if (!valido)
                return new ResultadoDto(false, Mensagens.ValidacaoClienteCamposInvalidos);

            if (ObterPorCpf(novoCliente.Login) != null)
                return new ResultadoDto(false, Mensagens.ValidacaoClienteCpfDuplicado);

            if (ObterPorLogin(novoCliente.Login) != null)
                return new ResultadoDto(false, Mensagens.ValidacaoClienteEmailDuplicado);

            return new ResultadoDto(true);
        }
    }
}
