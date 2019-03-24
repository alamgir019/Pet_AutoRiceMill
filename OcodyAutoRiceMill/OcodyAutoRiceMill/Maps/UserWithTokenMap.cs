using System.Linq;
using AutoMapper;
using OcodyAutoRiceMill.Api.Models.Users;
using OcodyAutoRiceMill.Data.Model;
using OcodyAutoRiceMill.Queries.Models;

namespace OcodyAutoRiceMill.Maps
{
    public class UserWithTokenMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<UserWithToken, UserWithTokenModel>();
        }
    }
}