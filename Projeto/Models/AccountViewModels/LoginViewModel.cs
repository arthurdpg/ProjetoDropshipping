using System.ComponentModel.DataAnnotations;
using Projeto.CrossCutting;

namespace Projeto.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Mensagens.CampoObrigatorio)]
        [EmailAddress(ErrorMessage = Mensagens.CampoFormato)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = Mensagens.CampoObrigatorio)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar?")]
        public bool RememberMe { get; set; }
    }
}
