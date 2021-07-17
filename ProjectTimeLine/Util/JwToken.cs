using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectTimeLine.Context;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeLine.Util
{
    public class JwToken
    {
        public IConfiguration _configuration;
        private readonly MyContext myContext;

        public JwToken()
        {
        }

        public JwToken(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public JwToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWT( IQueryable<DataLoginVM> loginVM)
        {
            var claims = new List<Claim>();

            var index = 0;
            foreach (var item in loginVM)
            {
                if (index == 0) claims.Add(new Claim("NIK", item.NIK));
                if (index == 0) claims.Add(new Claim("Email", item.Email));
                if (index == 0) claims.Add(new Claim("Name", item.Name));

                claims.Add(new Claim("Role", item.Role));
                index++;
            }
                        
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
