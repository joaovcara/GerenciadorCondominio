using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Mapeamentos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL
{
    public class Contexto : IdentityDbContext<Usuario, Funcao, string>
    {
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ServicoImovel> ServicoImoveis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AluguelMap());
            builder.ApplyConfiguration(new EventoMap());
            builder.ApplyConfiguration(new FuncaoMap());
            builder.ApplyConfiguration(new ImovelMap());
            builder.ApplyConfiguration(new MesMap());
            builder.ApplyConfiguration(new MovimentoMap());
            builder.ApplyConfiguration(new ServicoMap());
            builder.ApplyConfiguration(new ServicoImovelMap());
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new VeiculoMap());
            
        }
    }
}
