using FourCreate.Data;
using FourCreate.Data.Abstractions;
using FourCreate.Data.Repositories;
using FourCreate.Domain;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Interfaces;
using FourCreate.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("FourCreateDatabase");
builder.Services.AddDbContext<FourCreateDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<ISystemLogRepository, SystemLogRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();

builder.Services.AddTransient<ICreateEmployeeHandlerFactory,  CreateEmployeeHandlerFactory>();
builder.Services.AddTransient<CreateEmployeeHandler>();
builder.Services.AddTransient<AddEmployeeToCompanyHandler>();

builder.Services.AddTransient<ISystemLogService, SystemLogService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
