using AutoMapper;
using Entities.Model;
using Shared.DataTransferObject.Product;

namespace E_Commerce
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
