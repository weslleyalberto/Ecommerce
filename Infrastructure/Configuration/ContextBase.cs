using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
          
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraUsuario> CompraUsuarios { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConnection());
                base.OnConfiguring(optionsBuilder);
            }
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           /* foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }*/

            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            builder.Entity<Produto>().HasKey(t => t.Id);
          
            builder.Entity<Produto>().HasOne(c => c.ApplicationUsers);
            builder.Entity<CompraUsuario>().HasKey(t => t.Id);
            builder.Entity<CompraUsuario>().HasOne(c=> c.ApplicationUser).WithMany()
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.Entity<CompraUsuario>().HasOne(c=> c.Produto)
                .WithMany() .OnDelete(DeleteBehavior.Restrict).IsRequired();
        
            base.OnModelCreating(builder);
        }
        private string GetStringConnection() => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Teste;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


    }
}
