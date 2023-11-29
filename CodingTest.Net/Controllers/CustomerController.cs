using AutoMapper;
using CodingTest.Net.Model;
using CodingTest.Net.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace CodingTest.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<AtmController> Logger;
        private readonly ICustomerService CustomerService;
        private readonly IMapper Mappe;

        public CustomerController(ILogger<AtmController> logger, ICustomerService customerService, IMapper mapper)
        {
            Logger = logger;
            CustomerService = customerService;
            Mappe = mapper;
        }


        /// <summary>Save data from customers  <paramref name="customers"/> .</summary>
        /// <param name="customers"></param>
        /// <returns>Text about error ou sucess</returns>
        /// <remarks>
        ///     [
        ///         {
        ///     
        ///             firstName: 'Aaaa',
        ///             lastName: 'Bbbb',
        ///             age: 20,
        ///             id: 5
        ///     
        ///         },
        ///         {
        ///             firstName: 'Bbbb',
        ///             lastName: 'Cccc',
        ///             age: 24,
        ///             id: 6
        ///         }
        ///     ]
        ///
        /// </remarks>
        /// <response code="201">Returns a messagem of sucess</response>
        /// <response code="400">Returns a messagem of same error</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<string>> PostAsync([FromBody] CustomerDto[] customersDto)
        {
            var customersEntity = Mappe.Map <CustomerEntity[]>(customersDto);
            var errors = CustomerService.Valid(customersEntity);

                if (errors.Any())
                    return BadRequest(errors);

                await CustomerService.AddAsync(customersEntity);



            return Ok("Sucess");
        }

        /// <summary>Save data from customers  <paramref name="customers"/> .</summary>
        /// <param name="customers"></param>
        /// <returns>Text about error ou sucess</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="201">Returns a messagem of sucess</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public ActionResult<CustomerDto[]> GetAll()
        {
            var result = CustomerService.GetAll();

            var customerDto = Mappe.Map<CustomerDto[]>(result);
            return customerDto;
        }
    }
}
