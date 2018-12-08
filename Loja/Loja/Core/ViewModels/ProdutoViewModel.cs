using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Core.ViewModels
{
    public class ProdutoViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public decimal Preco { get; set; }
    }
}
