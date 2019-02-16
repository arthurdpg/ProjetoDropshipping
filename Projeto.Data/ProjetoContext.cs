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

        public virtual DbSet<Amigo> Amigos { get; set; }
        public virtual DbSet<Console> Consoles { get; set; }
        public virtual DbSet<Titulo> Titulos { get; set; }
        public virtual DbSet<Emprestimo> Emprestimos { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}
