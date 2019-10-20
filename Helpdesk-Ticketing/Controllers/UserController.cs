using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Helpdesk_Ticketing.Models;
using Helpdesk_Ticketing.Models.Interfaces;
using Helpdesk_Ticketing.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Helpdesk_Ticketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<AccountUsers> _userManager;
        private readonly SignInManager<AccountUsers> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;
        private readonly ICart _context;

        public UserController(ICart context, IUserService service, IConfiguration configuration, UserManager<AccountUsers> userManager, SignInManager<AccountUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Should get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserViewModel> All() => _service.All();

        /// <summary>
        /// rvm = register-view-model
        /// </summary>
        /// <param name="rvm"></param>
        /// <returns>POST Register User</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterUser rvm)
        {
            if (ModelState.IsValid)
            {
                AccountUsers user = new AccountUsers()
                {
                    //UserName = rvm.Email,
                    Email = rvm.Email
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);
                if (result.Succeeded)
                {
                    await _context.GetCartForUser(user.Email);

                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email);
                    List<Claim> claims = new List<Claim> { emailClaim };

                    await _userManager.AddClaimsAsync(user, claims);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if(user.Email == "admin@helpdeskteammember.com" || user.Email.Contains("@admin.com") || user.Email == "castillocarlosr2@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.MemberAdmin);
                    }
                    _service.Add(user);
                    //return RedirectToAction("Home", "ClientApp");
                    //return Ok(GenerateJwtToken(rvm.Email, user));
                    return Json(GenerateJwtToken(rvm.Email, user));
                }
            }
            //return View(rvm);
            return Json(View(rvm));
        }

        //[HttpGet]
        //public IActionResult Login() => View();

        /// <summary>
        /// POST Login User
        /// </summary>
        /// <param name="lvm">lvm = login-view-model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginUser lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    //var appUser = await _userManager.FindByEmailAsync(lvm.Email);
                    var appUser = _userManager.Users.SingleOrDefault(u => u.Email == lvm.Email);
                    if (await _userManager.IsInRoleAsync(appUser, ApplicationRoles.MemberAdmin))
                    {
                        var ourUser = await _userManager.FindByEmailAsync(lvm.Email);
                        return RedirectToPage("/Admin/Index");

                    }
                    //return RedirectToAction("Home", "ClientApp");
                    return Ok(GenerateJwtToken(lvm.Email, appUser));
                }
            }
            ModelState.TryAddModelError(string.Empty, "Invalid Login Attempt");
            return View(lvm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// TRrying JWT security claims for React.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}