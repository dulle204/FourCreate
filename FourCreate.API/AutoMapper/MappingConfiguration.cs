using AutoMapper;
using FourCreate.API.Models.Requests;
using FourCreate.Domain.Models;

namespace FourCreate.API.AutoMapper;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<CreateEmployeeRequest, CreateEmployee>();
        CreateMap<Models.Requests.NewEmployee, Domain.Models.NewEmployee>();
        CreateMap<CreateCompanyRequest, CreateCompany>();
    }
}
