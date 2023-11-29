using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Net.Service.Interfaces
{
    public interface IAtmService
    {
        List<string> Valid(int amount);

        List<string> CalculateOptionsPayOut(int amount);

    }
}
