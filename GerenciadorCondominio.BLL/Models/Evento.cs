using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Evento
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage ="Informe a descrição")]
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage ="Informe a data")]
        public DateTime Data { get; set; }
        public string Obsercacao { get; set; }

        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
