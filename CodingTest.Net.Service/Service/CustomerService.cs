using CodingTest.Net;
using CodingTest.Net.Domain.Interfaces;
using CodingTest.Net.Service.Interfaces;
using Microsoft.Extensions.Logging;


///  <summary>
///  Class for services about customer
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository CustomerRepository;

    private CustomerEntity[] internalArray = Array.Empty<CustomerEntity>();
    private readonly ILogger<CustomerService> Logger;


    ///  <summary>
    ///  Constructor of services customer
    /// </summary>
    public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
    {
        CustomerRepository = customerRepository;
        Logger = logger;
    }

    ///  <summary>
    ///  Return all the customers saved
    /// </summary>
    public CustomerEntity[]? GetAll()
    {
        return CustomerRepository.GetAll();

    }


    ///  <summary>
    ///  Add client ordering by last name and then first name
    /// </summary>
    public async Task AddAsync(CustomerEntity[] customers)
    {
        try
        {
            foreach (CustomerEntity customer in customers)
            {

                var resultIndex = internalArray.Any() ?
                internalArray.Select((s, i) => new { i, s })
                             .FirstOrDefault(t => string.Compare(t.s.LastName, customer.LastName, StringComparison.Ordinal) > 0)?.i :
                null;

                int index = resultIndex ?? internalArray.Length;

                internalArray = internalArray
                    .Take(index)
                    .Concat(new[] { customer })
                    .Concat(internalArray.Skip(index))
                    .ToArray();

                await CustomerRepository.SaveAsync(internalArray);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Erro at method {nameof(AddAsync)} ex: {ex}");
        }

    }

    ///  <summary>
    ///  Validate if the customer is valid for processing
    /// </summary>
    public List<string> Valid(CustomerEntity[] customers)
    {
        var errorList = new List<string>();
        try
        {
            if (customers.All(x => x.Id <= 0))
                errorList.Add("Id cannot be less than or equal to 0");

            if (customers.All(x => string.IsNullOrEmpty(x.FirstName)))
                errorList.Add("First Name cannot be empty or null");

            if (customers.All(x => string.IsNullOrEmpty(x.LastName)))
                errorList.Add("Last Name cannot be empty or null");

            if (customers.All(x => x.Age <= 18))
                errorList.Add("Age must be above 18");

            if (internalArray.IntersectBy(customers.Select(x => x.Id), x => x.Id).Any())
                errorList.Add("Id has been used before");
        }
        catch (Exception ex)
        {
            Logger.LogError($"Erro at method {nameof(Valid)} ex: {ex}");
        }

        return errorList;
    }
}
