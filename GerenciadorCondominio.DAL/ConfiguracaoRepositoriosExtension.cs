using GerenciadorCondominio.DAL.Interface;
using GerenciadorCondominio.DAL.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL
{
    public static class ConfiguracaoRepositoriosExtension
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IFuncaoRepositorio, FuncaoRepositorio>();
            services.AddTransient<IVeiculoRepositorio, VeiculoRepositorio>();
            services.AddTransient<IEventoRepositorio, EventoRepositorio>();
            services.AddTransient<IServicoRepositorio, ServicoRepositorio>();
            services.AddTransient<IServicoImovelRepositorio, ServicoImovelRepositorio>();
            services.AddTransient<IMovimentoRepositorio, MovimentoRepositorio>();
            services.AddTransient<IImovelRepositorio, ImovelRepositorio>();
            services.AddTransient<IMesRepositorio, MesRepositorio>();
            services.AddTransient<IAluguelRepositorio, AluguelRepositorio>();
            services.AddTransient<IPagamentoRepositorio, PagamentoRepositorio>();
        }
    }
}
