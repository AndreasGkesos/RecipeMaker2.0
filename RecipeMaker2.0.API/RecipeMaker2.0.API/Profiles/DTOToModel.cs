using AutoMapper;
using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Model;

namespace RecipeMaker2._0.API.Profiles
{
    public class DTOToModel : Profile
    {
        public DTOToModel()
        {
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<SupplierAddDTO, Supplier>();
            CreateMap<SupplierUpdateDTO, Supplier>();

            CreateMap<ProductDTO, Product>();
            CreateMap<ProductAddDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<MealDTO, Meal>()
                .ForMember(d => d.ProductQuantities, s => s.MapFrom(m => m.ProductQuantities));
            CreateMap<MealAddDTO, Meal>()
                .ForMember(d => d.ProductQuantities, s => s.MapFrom(m => m.ProductQuantities));
            CreateMap<MealUpdateDTO, Meal>()
                .ForMember(d => d.ProductQuantities, s => s.MapFrom(m => m.ProductQuantities));

            CreateMap<ProductQuantityDTO, ProductQuantity>();
            CreateMap<ProductQuantityAddDTO, ProductQuantity>();
            CreateMap<ProductQuantityUpdateDTO, ProductQuantity>();
        }
    }
}
