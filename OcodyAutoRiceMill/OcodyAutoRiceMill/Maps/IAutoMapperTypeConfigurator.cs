using AutoMapper;

namespace OcodyAutoRiceMill.Maps
{
    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfigurationExpression configuration);
    }
}