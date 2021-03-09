using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;
        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool IsUserExists(string userId)
        {
            var result = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (result != null)
                return true;
            return false;
        }

        public bool LoginUser(User user)
        {
            var result = _context.Users.FirstOrDefault(u => u.UserId == user.UserId && u.Password==user.Password);
            if (result != null)
                return true;
            return false;
        }
    }
}
