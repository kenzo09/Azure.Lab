using Loja.Core.Models;
using Loja.Core.Services;
using Loja.Infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoServices _produtoServices;

        public ProdutosController(IProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        public IActionResult Create()
        {
            var produto = new Produto
            {
                Id = 331556,
                Nome = "MI Mix 2",
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Celulares"
                },
                Descricao = "MI Mix 2 o melhor celular de todos.",
                Fabricante = new Fabricante
                {
                    Id = 1,
                    Nome = "Xiaomi"
                },
                Preco = 2000m,
                Tags = new[] { "xiaomi", "mimix", "celular" },
                ImagemPrincipalUrl = "https://img.staticbg.com/thumb/large/oaupload/banggood/images/4F/9F/0e579e00-1d90-419e-894d-48acea10f770.jpg"
            };

            //_azureStorage.AddProduto(produto);

            return Content("OK");
        }

        public async Task<IActionResult> List()
        {
            return Json(await _produtoServices.ObterProdutos());
        }
    }
}
