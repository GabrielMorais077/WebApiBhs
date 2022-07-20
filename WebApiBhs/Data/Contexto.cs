using Microsoft.EntityFrameworkCore;
using WebApiBhs.Domain;

namespace WebApiBhs.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Definindo id de produto como chave primaria
            modelBuilder.Entity<Produto>().HasKey(p => p.id);

        }
    }
}
