using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookLibrary.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Username { get; set; }

        [Required]
        [MinLength(4)]
        public string PasswordHash { get; set; }
    }
}
