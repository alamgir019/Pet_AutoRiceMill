using System.ComponentModel.DataAnnotations;

namespace OcodyAutoRiceMill.Api.Models.Users
{
    public class ChangeUserPasswordModel
    {
        [Required]
        public string Password { get; set; }
    }
}