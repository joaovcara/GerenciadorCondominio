﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.ViewModels
{
    public class FuncaoUsuarioViewModel
    {
        public string FuncaoId { get; set; }
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public bool IsSelected { get; set; }

    }
}
