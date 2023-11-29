using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Net.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerEntity[]? GetAll();
        Task SaveAsync(CustomerEntity[] customerEntity);
    }
}
