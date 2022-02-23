using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _dataContext;
        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Login(string userName, string Password)
        {
            using var hmac  = new HMACSHA1();

            var user = new AppUser()
            {
                UserName = userName,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password)),
                PasswordHash = hmac.Key
            };

            _dataContext.Users.Add(user);

            await _dataContext.SaveChangesAsync();

            return user;
        }
    }
}
