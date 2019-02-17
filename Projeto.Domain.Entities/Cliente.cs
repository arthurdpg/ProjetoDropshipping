using Projeto.CrossCutting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Column("CD_CLIENTE")]
        [Key]
        public int Codigo { get; set; }

        [Column("NM_CLIENTE")]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Column("NR_CPF")]
        [Required(AllowEmptyStrings = false)]
        public string Cpf { get; set; }

        [Column("DS_EMAIL")]
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Column("NM_USUARIO")]
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Column("NR_CELULAR")]
        [Required(AllowEmptyStrings = false)]
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
