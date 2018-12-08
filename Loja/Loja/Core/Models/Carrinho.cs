using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Core.Models
{
    public class Carrinho
    {
        public Carrinho()
        {
            Itens = new List<CarrinhoItem>();
        }

        public List<CarrinhoItem> Itens { get; set; }

        public void Add(Produto produto)
        {
            if (Itens.Any(c => c.Produto.Id == produto.Id))
            {
                var item = Itens.FirstOrDefault(c => c.Produto.Id == produto.Id);
                item.Quantidade++;
            }
            else
            {
                Itens.Add(new CarrinhoItem
                {
                    Produto = produto,
                    Quantidade = 1
                });
            }
        }
    }
}
