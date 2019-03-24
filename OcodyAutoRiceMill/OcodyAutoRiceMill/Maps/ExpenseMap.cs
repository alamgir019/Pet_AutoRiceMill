using System.Linq;
using AutoMapper;
using OcodyAutoRiceMill.Api.Models.Expenses;
using OcodyAutoRiceMill.Api.Models.Users;
using OcodyAutoRiceMill.Data.Model;

namespace OcodyAutoRiceMill.Maps
{
    public class ExpenseMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<Expense, ExpenseModel>();
            map.ForMember(x => x.Username, x => x.MapFrom(y => y.User.FirstName + " " + y.User.LastName));
        }
    }
}