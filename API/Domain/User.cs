using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class UserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }

        public User(string username, string hashPassword)
        {
            Username = username;
            HashPassword = hashPassword;
        }
    }
}
