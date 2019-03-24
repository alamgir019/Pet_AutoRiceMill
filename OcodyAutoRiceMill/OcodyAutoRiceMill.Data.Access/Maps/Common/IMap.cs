using Microsoft.EntityFrameworkCore;

namespace OcodyAutoRiceMill.Data.Access.Maps.Common
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}