using AutoMapper;
using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Model;

namespace RecipeMaker2._0.API.Profiles
{
    public class ModelToDTO : Profile
    {
        public ModelToDTO()
        {
            CreateMap<Supplier, SupplierDTO>();

            CreateMap<Product, ProductDTO>();

            CreateMap<ProductQuantity, ProductQuantityDTO>();

            CreateMap<Meal, MealDTO>()
                .ForMember(d => d.ProductQuantities, s => s.MapFrom(m => m.ProductQuantities));
        }
    }
}
