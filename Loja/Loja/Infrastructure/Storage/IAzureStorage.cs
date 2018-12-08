using Loja.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Infrastructure.Storage
{
    public interface IAzureStorage
    {
        void AddProduto(Produto produto);
        Task<List<Produto>> ObterProdutos();
    }
}