using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //-----------------------CRUD USANDO ENTITY--------------------- 
            //GravarUsandoAdoNet();
            //GravarUsandoEntity();
            //RecuperarProdutos();
            //DeletarProdutos();
            //RecuperarProdutos();
            //AtualizarProdutos();
            //RecuperarProdutos();

            //----------------------RELACIONAMENTOS---------------------------

            //UmParaMuitos();

            //MuitosParaMuitos();

            //InserirClienteComTabelaAninhada();

            //ConsultasComCondiçoes();

            //SelectComClausula();


            Console.ReadLine();
        }

        private static void SelectComClausula()
        {
            using (var contexto = new LojaContext())
            {
                var cliente = contexto.Clientes.Include(e => e.Endereco).FirstOrDefault();

                Console.WriteLine($"Endereco de entrega {cliente.Endereco.Logradouro}");

                /*
                    Com o objeto contexto, chamaremos o método Entry() passando a referência de produto.
                    Após, pegaremos a coleção da propriedade Compra com o método Collection(p => p.Compras).
                    Em seguida faremos uma Query(), que filtrará com a condição Where(c => c.Preco > 10).
                    Por último, carregaremos com Load() na referência passada no Entry(). 
                 */
                var produto = contexto
                    .Produtos
                    .Where(p => p.Id == 9004)
                    .FirstOrDefault();

                contexto.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.Preco > 10)
                    .Load();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine("\t" + item);
                }
            }
        }

        private static void ConsultasComCondiçoes()
        {
            void IncluirPromocao()
            {
                using (var context = new LojaContext())
                {
                    var promocao = new Promocao();
                    promocao.DataInicio = new DateTime(2020, 1, 1);
                    promocao.DataFim = new DateTime(2020, 5, 1);
                    promocao.Descricao = "Fim de Loja";

                    var produtos = context.Produtos.Where(p => p.Categoria == "Bebidas").ToList();

                    foreach (var item in produtos)
                    {
                        promocao.IncluiProduto(item);
                    }

                    context.Promocoes.Add(promocao);
                    context.SaveChanges();
                }
            };
            IncluirPromocao();
            using (var contexto2 = new LojaContext())
            {
                //Para que a pesquisa busque nas tabelas aninhadas, devemos incluir no select(como se faz em um JOIN)
                //var promocao = contexto2.Promocoes.FirstOrDefault();

                //Para relacionamentos N para M, devemos navegar nos niveis utilizando o include e theninclude
                var promocao = contexto2.Promocoes
                    .Include( p => p.Produtos )
                    .ThenInclude( pp => pp.Produto )
                    .FirstOrDefault();

                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void InserirClienteComTabelaAninhada()
        {
            var cliente = new Cliente();
            cliente.Nome = "Fulano da silva";
            cliente.Endereco = new Endereco() { Cidade = "Palmas", Logradouro = "Rua 10" };

            using (var context = new LojaContext())
            {
                context.Clientes.Add(cliente);
                context.SaveChanges();
            }
        }

        private static void AtualizarProdutos()
        {

            GravarUsandoEntity();
            using (var contexto = new ProdutoDAOEntity())
            {
                Produto produto = contexto.Lista().First();
                produto.Nome = "Nome alterado";
                contexto.Atualizar(produto);
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "gravado";
            p.Categoria = "Livros";
            p.PrecoUnitario = 25.86;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Lista().Add(p);
            }
        }

        private static void DeletarProdutos()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Lista();
                foreach (var item in produtos)
                {
                    contexto.Remover(item);
                }
            }
        }

        private static void RecuperarProdutos()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Lista();
                if (produtos.Count() == 0)
                {
                    Console.WriteLine("Lista vazia!!!");
                }
                foreach (var item in produtos)
                {
                    Console.WriteLine(item.Nome);
                }
            }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }

        private static void MuitosParaMuitos()
        {
            //----------------------MUITOS PARA MUITOS----------------------------

            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataFim = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                contexto.Promocoes.Add(promocaoDePascoa);

                var promocao = contexto.Promocoes.Find(1);
                contexto.Promocoes.Remove(promocao);

                contexto.SaveChanges();
            }

        }

        private static void UmParaMuitos()
        {
            //----------------------UM PARA MUITOS----------------------------

            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Frances";
            paoFrances.Categoria = "Padaria";
            paoFrances.Unidade = "Unidade";
            paoFrances.PrecoUnitario = 0.50;

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                contexto.Compras.Add(compra);
                 
                contexto.SaveChanges();
            }
        }
    }
}
