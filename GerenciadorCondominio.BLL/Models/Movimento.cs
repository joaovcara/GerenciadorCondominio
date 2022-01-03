﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Movimento
    {
        public int MovimentoId { get; set; }
        public decimal Valor { get; set; }
        public Tipos Tipo { get; set; }
        public int Dia { get; set; }
        public int MesId { get; set; }
        public virtual Mes Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataMovimento { get; set; }
    }

    public enum Tipos
    {
        Entrada,
        Saida
    }
}
