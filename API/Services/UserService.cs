using API.Data;
using API.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    public class UserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task Register(UserDTO userEntry)
        {
            if (await _context.Users.AnyAsync(u => u.Username == userEntry.Username))
            {
                throw new System.Exception("Usuário já existe");
            }

            string hashPassword = HashPassword(userEntry.Password);
            Console.WriteLine(hashPassword);
            User user = new (userEntry.Username, hashPassword);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Get(string userName, string userPassword)
        {
            string hashedPassword = HashPassword(userPassword);
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName && u.HashPassword == hashedPassword);
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
