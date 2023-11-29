using CodingTest.Net.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodingTest.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        IAtmService AtmService;

        public AtmController(ILogger<AtmController> logger, IAtmService atmService)
        {
            AtmService = atmService;
        }


        /// <summary>Calculate  <paramref name="amount"/> .</summary>
        /// <param name="amount">Amount</param>
        /// <returns>List of String</returns>
        /// <remarks>
        ///     {
        ///         220
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the possibilites of payout</response>
        /// <response code="400">If the number sent was invalid</response>

        [HttpPost("Payout")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>),StatusCodes.Status400BadRequest)]
        public ActionResult<List<string>> Post([FromBody] int amount)
        {
            var errors = AtmService.Valid(amount);

            if (errors.Any())
                return BadRequest(errors);

            var result = AtmService.CalculateOptionsPayOut(amount);

            return Ok(result);
        }

      

    }

}
