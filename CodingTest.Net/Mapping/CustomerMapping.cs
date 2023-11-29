using CodingTest.Net.Model;
using AutoMapper;

namespace CodingTest.Net.Application.Mapping
{
    ///  <summary>
    ///  Class for mapping the field of customer
    /// </summary>
    public class CustomerMapping : Profile
    {
        ///  <summary>
        ///  Constructor of mapping customer
        /// </summary>
        public CustomerMapping()
        {
            CreateMap<CustomerDto, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerDto>();
        }
    }
}

