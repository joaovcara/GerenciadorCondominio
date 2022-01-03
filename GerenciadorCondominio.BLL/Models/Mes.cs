using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Mes
    {
        public int MesId { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Aluguel> Alugueis { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
    }
}
