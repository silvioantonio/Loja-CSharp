using System;
using Microsoft.EntityFrameworkCore;

namespace Alura.Loja.Testes.ConsoleApp
{
    class LojaContext : DbContext
    {
        // Classe a ser persistida pela entity, no caso, produto..o nome da propriedade sera o nome da tabela, no caso Produtos
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // No momento de criar minha migração e gerar a nova tabela intermediaria, eu coloco  esses dois parametros como 
            //idetificadores unicos da nova tabela, nao precisando declarar um campo ID unico(oque nao fari sentido no contexto)
            modelBuilder.Entity<PromocaoProduto>().HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });

            modelBuilder.Entity<Endereco>().ToTable("Enderecos");

            //Dessa forma informamos que o id do endereco é o mesmo id do cliente(Conhecido como estado de sombra)
            modelBuilder.Entity<Endereco>().Property<int>("ClienteId");

            modelBuilder.Entity<Endereco>().HasKey("ClienteId");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LojaDB;Trusted_Connection=true;";
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
