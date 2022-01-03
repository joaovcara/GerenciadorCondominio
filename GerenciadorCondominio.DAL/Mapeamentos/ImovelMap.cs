using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class ImovelMap : IEntityTypeConfiguration<Imovel>
    {
        public void Configure(EntityTypeBuilder<Imovel> builder)
        {
            builder.HasKey(x => x.ImovelId);
            builder.Property(x => x.Descricao).IsRequired();
            builder.Property(x => x.Endereco).IsRequired();
            builder.Property(x => x.Numero).IsRequired();
            builder.Property(x => x.Bairro).IsRequired();
            builder.Property(x => x.Cidade).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.ProprietarioId).IsRequired();
            builder.Property(x => x.LocatarioId).IsRequired(false);

            builder.HasOne(x => x.Proprietario).WithMany(x => x.ProprietariosImoveis).HasForeignKey(x => x.ProprietarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Locatario).WithMany(x => x.LocadoresImoveis).HasForeignKey(x => x.LocatarioId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Imoveis");
        }
    }
}
