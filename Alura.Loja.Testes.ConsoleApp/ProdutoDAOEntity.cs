using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class ProdutoDAOEntity : ICrudDAO<Produto>, IDisposable
    {
        private LojaContext contexto;

        public ProdutoDAOEntity()
        {
            contexto = new LojaContext();
        }

        public void Adicionar(Produto t)
        {
            contexto.Produtos.Add(t);
            contexto.SaveChanges();
        }

        public void Atualizar(Produto t)
        {
            contexto.Produtos.Update(t);
            contexto.SaveChanges();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public IList<Produto> Lista()
        {
            return contexto.Produtos.ToList();
        }

        public void Remover(Produto t)
        {
            contexto.Produtos.Remove(t);
            contexto.SaveChanges();
        }
    }
}
