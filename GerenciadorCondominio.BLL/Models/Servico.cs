using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Servico
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public StatusServico Status { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public ICollection<ServicoImovel> ServicoImoveis { get; set; }
    }

    public enum StatusServico
    {
        Pendente,
        Recusado,
        Aceito
    }
}
