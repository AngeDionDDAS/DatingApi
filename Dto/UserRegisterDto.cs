using System.ComponentModel.DataAnnotations;

namespace DatingApi.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage ="You must specify password between 4 and 8")]
        public string Password { get; set; }    
    }
}
