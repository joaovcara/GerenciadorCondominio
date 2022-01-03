using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class MesMap : IEntityTypeConfiguration<Mes>
    {
        public void Configure(EntityTypeBuilder<Mes> builder)
        {
            builder.HasKey(x => x.MesId);
            builder.Property(x => x.MesId).ValueGeneratedNever();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Descricao).IsUnique();

            builder.HasMany(x => x.Alugueis).WithOne(x => x.Mes);
            builder.HasMany(x => x.Movimentos).WithOne(x => x.Mes);

            builder.HasData(
                new Mes
                {
                    MesId = 1,
                    Descricao = "Janeiro"
                },
                new Mes
                {
                    MesId = 2,
                    Descricao = "Fevereiro"
                },
                new Mes
                {
                    MesId = 3,
                    Descricao = "Março"
                },
                new Mes
                {
                    MesId = 4,
                    Descricao = "Abril"
                },
                new Mes
                {
                    MesId = 5,
                    Descricao = "Maio"
                },
                new Mes
                {
                    MesId = 6,
                    Descricao = "Junho"
                },
                new Mes
                {
                    MesId = 7,
                    Descricao = "Julho"
                },
                new Mes
                {
                    MesId = 8,
                    Descricao = "Agosto"
                },
                new Mes
                {
                    MesId = 9,
                    Descricao = "Setembro"
                },
                new Mes
                {
                    MesId = 10,
                    Descricao = "Outubro"
                },
                new Mes
                {
                    MesId = 11,
                    Descricao = "Novembro"
                },
                new Mes
                {
                    MesId = 12,
                    Descricao = "Dezembro"
                });

            builder.ToTable("Meses");

        }
    }
}
