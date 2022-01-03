using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(x => x.EventoId);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Obsercacao);
            builder.Property(x => x.UsuarioId).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(x => x.Eventos).HasForeignKey(x => x.UsuarioId);

            builder.ToTable("Eventos");
        }
    }
}
