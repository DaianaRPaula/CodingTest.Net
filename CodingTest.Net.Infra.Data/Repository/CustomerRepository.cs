using CodingTest.Net.Domain.Interfaces;
using CodingTest.Net.Infra.Data.Context;
using System.Text.Json;

namespace CodingTest.Net.Infra.Data.Repository
{
    ///  <summary>
    ///  Class for repository about customer
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly ArchiveJsonContext ArchiveJsonContext;

        ///  <summary>
        ///  Constructor of repository customer
        /// </summary>
        public CustomerRepository(ArchiveJsonContext archiveJsonContext) {
            ArchiveJsonContext = archiveJsonContext;
        }

        ///  <summary>
        ///  Get all customer from context
        /// </summary>
        public CustomerEntity[]? GetAll()
        {
            return JsonSerializer.Deserialize<CustomerEntity[]>(ArchiveJsonContext.dataText);
        }

        ///  <summary>
        ///  Save all customer into context
        /// </summary>
        public async Task SaveAsync(CustomerEntity[] customerEntity)
        {
            await ArchiveJsonContext.SaveIntoFileAsync(customerEntity);
        }
    }
}
