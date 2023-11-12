using AutoMapper;
using FourCreate.API.Models.Requests;
using FourCreate.Domain;

namespace FourCreate.API.AutoMapper;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<CreateEmployeeRequest, CreateEmployee>();
        CreateMap<Models.Requests.NewEmployee, Domain.NewEmployee>();
        CreateMap<CreateCompanyRequest, CreateCompany>();
    }
}
