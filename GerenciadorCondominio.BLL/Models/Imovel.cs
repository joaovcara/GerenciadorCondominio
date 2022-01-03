using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Imovel
    {
        public int ImovelId { get; set; }

        [Required(ErrorMessage ="Informe a descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe o numero")]
        [Display(Name ="Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o estado")]
        public string Estado { get; set; }

        public string Foto { get; set; }

        public string LocatarioId { get; set; }
        public virtual Usuario Locatario { get; set; }

        public string ProprietarioId { get; set; }
        public virtual Usuario Proprietario { get; set; }

    }
}
