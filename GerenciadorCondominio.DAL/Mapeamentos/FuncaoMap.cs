using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(30);

            builder.HasData(
                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Locatário",
                    NormalizedName = "LOCATARIO",
                    Descricao = "Locatário do Imóvel"
                },

                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Responsável",
                    NormalizedName = "RESPONSÁVEL",
                    Descricao = "Responsável do Imóvel"
                },

                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    Descricao = "Administrador do Imóvel"
                });

            builder.ToTable("Funcoes");

        }
    }
}
