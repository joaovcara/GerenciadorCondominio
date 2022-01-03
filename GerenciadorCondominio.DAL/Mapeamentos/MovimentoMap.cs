using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class MovimentoMap : IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.HasKey(x => x.MovimentoId);
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.DataMovimento).IsRequired();
            builder.Property(x => x.Dia).IsRequired();
            builder.Property(x => x.MesId).IsRequired();
            builder.Property(x => x.Ano).IsRequired();

            builder.HasOne(x => x.Mes).WithMany(x => x.Movimentos).HasForeignKey(x => x.MesId);

            builder.ToTable("Movimentos");
        }
    }
}
