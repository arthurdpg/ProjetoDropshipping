using Projeto.CrossCutting;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = Mensagens.CampoObrigatorio)]
        [EmailAddress(ErrorMessage = Mensagens.CampoFormato)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
