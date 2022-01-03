using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class ServicoImovel
    {
        public int ServicoImovelId { get; set; }
        public int ServicoId { get; set; }
        public virtual Servico Servico { get; set; }
        public DateTime DataExecucao { get; set; }
    }
}
