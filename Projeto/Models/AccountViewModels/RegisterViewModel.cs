using System.ComponentModel.DataAnnotations;
using Projeto.CrossCutting;

namespace Projeto.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = Mensagens.CampoObrigatorio)]
        [EmailAddress(ErrorMessage = Mensagens.CampoFormato)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = Mensagens.CampoObrigatorio)]
        [StringLength(100, ErrorMessage = Mensagens.CampoComprimento, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Password", ErrorMessage = Mensagens.CampoSenhaEConfirmacao)]
        public string ConfirmPassword { get; set; }
    }
}
