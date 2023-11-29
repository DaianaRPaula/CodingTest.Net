using CodingTest.Net.Service.Interfaces;
using Microsoft.Extensions.Logging;


///  <summary>
///  Class for services about atm
/// </summary>
public class AtmService : IAtmService
{

    private static ILogger<CustomerService> Logger;

    ///  <summary>
    ///  Constructor of services atm
    /// </summary>
    public AtmService(ILogger<CustomerService> logger)
	{
        Logger = logger;

    }

    ///  <summary>
    ///  Calculate the options of payout of a number
    /// </summary>
    public List<string> CalculateOptionsPayOut(int amount)
    {
        var result = new List<string>();
        try
        {

            result.Add($"10 X {(amount / 10)} EUR");

            if (amount >= 50)
            {
                AddDenomination(result, amount, 50);
            }

            if (amount >= 100)
            {
                AddDenomination(result, amount, 100);
            }

        }
        catch (Exception ex)
        {
            Logger.LogError($"Erro at method {nameof(CalculateOptionsPayOut)} ex: {ex}");
        }

        return result;
    }

    ///  <summary>
    ///  Calculate the options the principal values
    /// </summary>
    private static void AddDenomination(List<string> result, int amount, int denomination)
    {
        try
        {

            var totalPayout = amount / denomination;
            var remainingAmount = amount % denomination;

            if (remainingAmount == 0)
            {
                result.Add($"{denomination} X {totalPayout} EUR");
            }
            else
            {
                result.Add($"{denomination} X {totalPayout} EUR + 10 X {remainingAmount / 10} EUR");
                AddRemainingOptions(result, amount, denomination, remainingAmount);
            }

        }
        catch (Exception ex)
        {
            Logger.LogError($"Erro at method {nameof(AddDenomination)} ex: {ex}");
        }
    }

    ///  <summary>
    ///  Calculate the diferent options values
    /// </summary>
    private static void AddRemainingOptions(List<string> result, int amount, int denomination, int remainingAmount)
    {
        try
        {
            var count = 1;
            var slipOptionAmount = amount - denomination;

            while (slipOptionAmount > denomination)
            {
                result.Add($"{denomination} X {count} EUR + 10 X {slipOptionAmount / 10} EUR");
                slipOptionAmount -= denomination;
                count++;
            }

            if (slipOptionAmount > 0 && remainingAmount == 0)
            {
                if (count > 1)
                    count++;
                result.Add($"{denomination} X {count} EUR + 10 X {slipOptionAmount / 10} EUR");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Erro at method {nameof(AddRemainingOptions)} ex: {ex}");
        }
    }

    ///  <summary>
    ///  Validate if the customer is valid for processing
    /// </summary>
    public List<string> Valid(int amount)
    {
        var error = new List<string>();
        if (amount <= 0 || amount % 10 > 0)
        {
            error.Add("The number is negative or has values in the last space that are different from zero");
        }
        return error;
    }
}
