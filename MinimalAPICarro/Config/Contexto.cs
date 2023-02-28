using Microsoft.EntityFrameworkCore;
using MinimalAPICarro.Modelo;

namespace MinimalAPICarro.Config
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            //Cria o banco de dados caso não exista 
             Database.EnsureCreated(); 
        }

        public DbSet<Carros> Carro { get; set; }


    }
}
