using AutoMapper;
using FourCreate.API.Models.Requests;
using FourCreate.Data.Models;
using FourCreate.Domain;
using FourCreate.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FourCreate.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    private readonly IMapper mapper;

    public EmployeeController(
        IEmployeeService employeeService,
        IMapper mapper)
    {
        this.employeeService = employeeService;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateEmployeeRequest request)
    {
        var createEmploee = mapper.Map<CreateEmployee>(request);
        var result = await employeeService.CreateEmployee(createEmploee);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Entity);
    }
}
