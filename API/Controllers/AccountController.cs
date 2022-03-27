using API.Data;
using API.Dto;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public AccountController(DataContext dataContext, ITokenService tokenService, IUserRepository userRepository)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserNameExists(registerDto.userName)) 
               return BadRequest("Username already exists.");
            using var hmac  = new HMACSHA512();

            var user = new AppUser()
            {
                UserName = registerDto.userName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password.ToString())),
                PasswordSalt = hmac.Key
            };

            _dataContext.Users.Add(user);

            await _dataContext.SaveChangesAsync();

            return new UserDto { userName = user.UserName, token = _tokenService.CreateToken(user)};
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _dataContext.Users.Where(s => s.UserName == loginDto.userName.ToLower()).FirstOrDefaultAsync();

            if (user == null) return Unauthorized("Not authorized");
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != user.PasswordHash[i]) return Unauthorized("Wrong creds."); 
            }

            return new UserDto { userName = user.UserName, token = _tokenService.CreateToken(user) };
        }


        #region
        private async Task<bool> UserNameExists(string userName)
        {
            return await _dataContext.Users.AnyAsync(s => s.UserName == userName.ToLower());
        }
        #endregion
    }
}
