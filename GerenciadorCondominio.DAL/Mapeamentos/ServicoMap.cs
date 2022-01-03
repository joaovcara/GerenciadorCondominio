using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.Property(x => x.ServicoId);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UsuarioId).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(x => x.Servicos).HasForeignKey(x => x.UsuarioId);
            builder.HasMany(x => x.ServicoImoveis).WithOne(x => x.Servico);

            builder.ToTable("Servicos");
        }
    }
}
