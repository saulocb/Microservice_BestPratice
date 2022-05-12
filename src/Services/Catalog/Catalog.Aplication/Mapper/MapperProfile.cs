using AutoMapper;
using Catalog.Aplication.Features.Product.Commands.CreateProduct;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Domain.Entities;


namespace Catalog.Aplication.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, ProductsVm>().ReverseMap();
        }
    }
}
