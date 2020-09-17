using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class PromocaoProduto
    {
        public Produto Produto { get; set; }
        public Promocao Promocao { get; set; }
        public int ProdutoId { get; set; }
        public int PromocaoId { get; set; }
    }
}
