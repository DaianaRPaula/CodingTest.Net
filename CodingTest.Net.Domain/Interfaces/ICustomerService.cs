using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Net.Service.Interfaces
{
    public interface ICustomerService
    {
        List<string> Valid(CustomerEntity[] customers);
        Task AddAsync(CustomerEntity[] customers);

        CustomerEntity[]? GetAll();
    }
}
