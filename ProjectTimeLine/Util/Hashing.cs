using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Util
{
    public class Hashing
    {
        public IConfiguration _configuration;

        public Hashing()
        {
        }

        public Hashing(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }

        public static string GetGUID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
