using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Promocao
    {
        public int Id { get; set; }
        public DateTime DataFim { get; internal set; }
        public DateTime DataInicio { get; internal set; }
        public string Descricao { get; internal set; }
        public IList<PromocaoProduto> Produtos { get; set; }

        public Promocao()
        {
            this.Produtos = new List<PromocaoProduto>();
        }

        public void IncluiProduto(Produto produto)
        {
            //Relacioos objetos à tabela PromocaoProduto
            this.Produtos.Add(new PromocaoProduto() { Produto = produto });
        }
    }
}
