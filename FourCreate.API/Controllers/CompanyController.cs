using AutoMapper;
using FourCreate.API.Models.Requests;
using FourCreate.Domain;
using FourCreate.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FourCreate.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService companyService;
    private readonly IMapper mapper;

    public CompanyController(
        ICompanyService companyService,
        IMapper mapper)
    {
        this.companyService = companyService;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCompany(CreateCompanyRequest request)
    {
        var createCompany = mapper.Map<CreateCompany>(request);
        var result = await companyService.CreateCompany(createCompany);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result);
    }
}
