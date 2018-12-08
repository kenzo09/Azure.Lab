using AutoMapper;
using Loja.Core.Models;
using Loja.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Infrastructure.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(p => p.Id, vm => vm.MapFrom(pvm => pvm.Id))
                .ForMember(p => p.Nome, vm => vm.MapFrom(pvm => pvm.Nome))
                .ForMember(p => p.Preco, vm => vm.MapFrom(pvm => pvm.Preco))
                .ForMember(p => p.ImagemUrl, vm => vm.MapFrom(pvm => pvm.ImagemPrincipalUrl));
        }
    }
}
