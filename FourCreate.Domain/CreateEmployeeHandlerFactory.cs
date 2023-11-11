using FourCreate.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourCreate.Domain;
public class CreateEmployeeHandlerFactory
{
    private readonly List<ICreateEmployeeHandler> createEmployeeHandlers;

    public CreateEmployeeHandlerFactory()
    {
        
    }
}
