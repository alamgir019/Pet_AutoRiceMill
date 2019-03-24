using System;

namespace OcodyAutoRiceMill.Security.Auth
{
    public interface ITokenBuilder
    {
        string Build(string name, string[] roles, DateTime expireDate);
    }
}