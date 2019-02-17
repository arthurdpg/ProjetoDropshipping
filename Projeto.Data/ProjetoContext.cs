using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;

namespace Projeto.Data
{
    public class ProjetoContext : IdentityDbContext<ApplicationUser>
    {
        public ProjetoContext(DbContextOptions<ProjetoContext> options) : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Promocao> Promocoes { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
    }
}
