﻿using Microsoft.EntityFrameworkCore;
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

            //----------------------MUITOS PARA MUITOS----------------------------

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Pascoa Da Hora";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataFim = DateTime.Now.AddMonths(3);

            promocaoDePascoa.Produtos.Add(new Produto());
            promocaoDePascoa.Produtos.Add(new Produto());
            promocaoDePascoa.Produtos.Add(new Produto());

            Console.ReadLine();
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
    }
}
