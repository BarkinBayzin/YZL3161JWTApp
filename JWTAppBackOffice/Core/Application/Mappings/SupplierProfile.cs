using AutoMapper;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;

namespace JWTAppBackOffice.Core.Application.Mappings
{
    public class SupplierProfile:Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierListDto>().ReverseMap();
        }
    }
}
