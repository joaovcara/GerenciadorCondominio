using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.PrimeiroAcesso).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.HasMany(x => x.ProprietariosImoveis).WithOne(x => x.Proprietario);
            builder.HasMany(x => x.LocadoresImoveis).WithOne(x => x.Locatario);
            builder.HasMany(x => x.Veiculos).WithOne(x => x.Usuario);
            builder.HasMany(x => x.Eventos).WithOne(x => x.Usuario);
            builder.HasMany(x => x.Pagamentos).WithOne(x => x.Usuario);
            builder.HasMany(x => x.Servicos).WithOne(x => x.Usuario);

            builder.ToTable("Usuarios");
        }
    }
}
