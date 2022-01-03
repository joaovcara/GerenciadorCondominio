using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class ServicoImovelMap : IEntityTypeConfiguration<ServicoImovel>
    {
        public void Configure(EntityTypeBuilder<ServicoImovel> builder)
        {
            builder.HasKey(x => x.ServicoImovelId);
            builder.Property(x => x.ServicoId).IsRequired();
            builder.Property(x => x.DataExecucao).IsRequired();

            builder.HasOne(x => x.Servico).WithMany(x => x.ServicoImoveis).HasForeignKey(x => x.ServicoId);

            builder.ToTable("ServicoImoveis");
        }
    }
}
