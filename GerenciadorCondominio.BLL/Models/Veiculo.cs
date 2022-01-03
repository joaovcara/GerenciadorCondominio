using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Veiculo
    {
        public int VeiculoId { get; set; }

        [Required(ErrorMessage ="Informe a descrição")]
        [StringLength(20, ErrorMessage = "Máximo 20 Caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a marca")]
        [StringLength(20, ErrorMessage = "Máximo 20 Caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Informe a cor")]
        [StringLength(20, ErrorMessage = "Máximo 20 Caracteres")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "Informe a placa")]
        public string Placa { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
