using AutoMapper;
using Loja.Core.Models;
using Loja.Core.Services;
using Loja.Core.ViewModels;
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
    public class CarrinhoController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        private readonly ICarrinhoService _carrinhoService;
        private readonly IMapper _mapper;

        public CarrinhoController(IProdutoServices produtoServices, ICarrinhoService carrinhoService, IMapper mapper)
        {
            _produtoServices = produtoServices;
            _carrinhoService = carrinhoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Add(int id)
        {
            var usuario = HttpContext.User.Identity.Name;

            var carrinho = _carrinhoService.Obter(usuario);

            carrinho.Add(await _produtoServices.ObterProduto(id));

            _carrinhoService.Salvar(usuario, carrinho);

            return PartialView("Index", carrinho);
        }

        public IActionResult Finalizar()
        {
            var usuario = HttpContext.User.Identity.Name;
            var carrinho = _carrinhoService.Obter(usuario);

            _carrinhoService.Limpar(usuario);

            return View(carrinho);
        }
    }
}
