using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(40, ErrorMessage = "Máximo 40 caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        public string Telefone { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(40, ErrorMessage = "Máximo 40 caractéres")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(40, ErrorMessage = "Máximo 40 caractéres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(40, ErrorMessage = "Máximo 40 caractéres")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string SenhaConfirmada { get; set; }

    }
}
