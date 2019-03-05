using System.ComponentModel.DataAnnotations;

namespace AutoRiceMill.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}