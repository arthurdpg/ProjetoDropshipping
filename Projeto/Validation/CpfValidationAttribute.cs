using Projeto.CrossCutting;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Validation
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value == null || DocumentosHelper.ValidarCpf(value.ToString()) ? ValidationResult.Success : 
                new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
        }
    }
}
