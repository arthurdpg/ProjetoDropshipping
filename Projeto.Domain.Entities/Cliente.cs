using Projeto.CrossCutting;

namespace Projeto.Domain.Entities
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Celular { get; set; }

        public bool Validar()
        {
            bool valido = true;

            valido &= Nome != null && !string.IsNullOrEmpty(Nome.Trim());
            valido &= Cpf != null && !string.IsNullOrEmpty(Cpf.Trim()) && DocumentosHelper.ValidarCpf(Cpf);
            valido &= Email != null && !string.IsNullOrEmpty(Email.Trim());
            valido &= Login != null && !string.IsNullOrEmpty(Login.Trim());
            valido &= Celular != null && !string.IsNullOrEmpty(Celular.Trim());

            return valido;
        }
    }
}
