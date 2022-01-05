using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.ViewModels
{
    public class ServicoAprovadoViewModel
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name= "Data de Execução")]
        public DateTime Data { get; set; }
    }
}
