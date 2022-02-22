using BookStore_API.Contracts;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;

        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILoggerService logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        /// <summary>
        /// User login endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var username = userDTO.UserName;
            var password = userDTO.Password;
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if(result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                return Ok(user);
            }

            return Unauthorized(userDTO);
        }
    }
}
