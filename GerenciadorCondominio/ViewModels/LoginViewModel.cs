using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

    }
}
