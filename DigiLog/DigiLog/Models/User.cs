using System.ComponentModel.DataAnnotations;

namespace DigiLog.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;

        //public DateTime DateCreated { get; set; }
        //public DateTime DateModified { get; set; }

    }
}
