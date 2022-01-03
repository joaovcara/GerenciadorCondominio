using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(x => x.VeiculoId);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Cor).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Marca).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Placa).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Placa).IsUnique();
            builder.Property(x => x.UsuarioId).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(x => x.Veiculos).HasForeignKey(x => x.UsuarioId);

            builder.ToTable("Veiculos");
        }
    }
}
