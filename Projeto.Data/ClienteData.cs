using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using System.Linq;

namespace Projeto.Data
{
    public class ClienteData : CrudData<Cliente>, IClienteData
    {
        public ClienteData(ProjetoContext context) : base(context)
        {
        }

        public Cliente ObterPorLogin(string login)
        {
            return Context.Clientes.Where(c => c.Login.Equals(login)).FirstOrDefault();
        }

        public Cliente ObterPorCpf(string cpf)
        {
            return Context.Clientes.Where(c => c.Cpf.Equals(cpf)).FirstOrDefault();
        }
    }
}
