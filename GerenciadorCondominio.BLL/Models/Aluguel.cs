using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Aluguel
    {
        public int AluguelId { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        [Range(0,int.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        [Display(Name = "Mês")]
        public int MesId { get; set; }
        public Mes Mes { get; set; }

        [Required(ErrorMessage = "Informe o ano")]
        [Range(2000, int.MaxValue, ErrorMessage = "O ano deve ser maior que zero")]
        public int Ano { get; set; }

        public virtual ICollection<Pagamento> Pagamentos { get; set; }

    }
}
