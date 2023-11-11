using FourCreate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourCreate.Domain.Abstractions;
public interface ICreateEmployeeHandler
{
    bool EmployeeExists { get; }
    DomainResult<Employee> HnadleCreae(CreateEmployee employee);
}
