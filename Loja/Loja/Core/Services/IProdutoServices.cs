using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Core.Models;

namespace Loja.Core.Services
{
    public interface IProdutoServices
    {
        Task<List<Produto>> ObterProdutos();
        Task<Produto> ObterProduto(int id);
    }
}