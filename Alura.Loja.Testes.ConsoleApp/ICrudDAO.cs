using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    interface ICrudDAO<T>
    {
        void Adicionar(T t);
        void Atualizar(T t);
        void Remover(T t);
        IList<T> Lista();
    }
}
