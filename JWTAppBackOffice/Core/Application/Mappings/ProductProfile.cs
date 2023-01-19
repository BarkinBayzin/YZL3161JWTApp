using AutoMapper;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;

namespace JWTAppBackOffice.Core.Application.Mappings
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
