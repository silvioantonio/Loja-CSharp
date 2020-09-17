using System;
using Microsoft.EntityFrameworkCore;

namespace Alura.Loja.Testes.ConsoleApp
{
    class LojaContext : DbContext
    {
        // Classe a ser persistida pela entity, no caso, produto..o nome da propriedade sera o nome da tabela, no caso Produtos
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LojaDB;Trusted_Connection=true;";
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
