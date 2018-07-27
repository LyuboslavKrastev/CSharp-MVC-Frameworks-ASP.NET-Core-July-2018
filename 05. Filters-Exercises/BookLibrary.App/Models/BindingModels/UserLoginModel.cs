using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Models.BindingModels
{
    public class UserLoginModel
    {
        [Required]
        [MinLength(2)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
