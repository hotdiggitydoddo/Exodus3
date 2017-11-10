using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exodus3.Api.Helpers;
using Exodus3.Api.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Exodus3.Api.Data.Entities;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Exodus3.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        private static bool _databaseChecked;
        private readonly ILogger _logger;

        public TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                  ILoggerFactory loggerfactory, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = loggerfactory.CreateLogger<AccountsController>();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("e3IrvLa4Jesus4ever!"))
                                .AddSubject(user.Email)
                                .AddIssuer("Exodus3.Security.Bearer")
                                .AddAudience("Exodus3.Security.Bearer")
                                .AddClaim("roles", string.Join(",", roles))
                            //    .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
                                .Build();

                        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        //var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        //_config["Tokens:Issuer"],
                        //claims,
                        //expires: DateTime.Now.AddMinutes(30),
                        //signingCredentials: creds);

                        //await _roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                     
                        return Ok(token.Value);
                    }
                }
            }

            return BadRequest("Could not create token");
        }
        //[HttpPost]
        //public IActionResult Create(LoginViewModel model)
        //{
            
        //    //if (inputModel.Username != "james" && inputModel.Password != "bond")
        //        //return Unauthorized();

        //    var token = new JwtTokenBuilder()
        //                        .AddSecurityKey(JwtSecurityKey.Create("fiversecret "))
        //                        .AddSubject("james bond")
        //                        .AddIssuer("Fiver.Security.Bearer")
        //                        .AddAudience("Fiver.Security.Bearer")
        //                        .AddClaim("MembershipId", "111")
        //                        .AddExpiry(1)
        //                        .Build();

        //    return Ok(token.Value);
        //}
    }
}
