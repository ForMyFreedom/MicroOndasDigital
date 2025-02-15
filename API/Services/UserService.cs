using API.Data;
using API.Domain;
using Microsoft.EntityFrameworkCore;
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

            string hashPassword = CryptoService.HashPassword(userEntry.Password);
            User user = new (userEntry.Username, hashPassword);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Get(string userName, string userPassword)
        {
            string hashedPassword = CryptoService.HashPassword(userPassword);
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName && u.HashPassword == hashedPassword);
        }
    }
}
