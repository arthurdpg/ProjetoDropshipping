using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projeto.Data;
using Projeto.Data.Interfaces;
using Projeto.Domain;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.DependencyResolver
{
    public static class MvcResolver
    {
        public static void Init(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProjetoContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ProjetoContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAmigoBusiness, AmigoBusiness>();
            services.AddTransient<IAmigoData, AmigoData>();

            services.AddTransient<IConsoleBusiness, ConsoleBusiness>();
            services.AddTransient<IConsoleData, ConsoleData>();

            services.AddTransient<ITituloBusiness, TituloBusiness>();
            services.AddTransient<ITituloData, TituloData>();

            services.AddTransient<IEmprestimoBusiness, EmprestimoBusiness>();
            services.AddTransient<IEmprestimoData, EmprestimoData>();
            services.AddTransient<IEmprestimoAmigoTituloBusiness, EmprestimoAmigoTituloBusiness>();

            services.AddTransient<IClienteBusiness, ClienteBusiness>();
            services.AddTransient<IPromocaoBusiness, PromocaoBusiness>();

            services.AddTransient<IClienteData, ClienteData>();
        }
    }
}
