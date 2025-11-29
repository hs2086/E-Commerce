using AutoMapper;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObject.Category;
using Shared.DataTransferObject.Order;
using Shared.DataTransferObject.Product;
using Shared.DataTransferObject.Role;

namespace E_Commerce
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<CreateProductDto, Product>()
                    .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

            CreateMap<CreateCategoryDto, Category>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore());

            CreateMap<UpdateCategoryDto, Category>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<IdentityRole, RoleDto>();
            CreateMap<Order, OrderDto>();
        }
    }
}
