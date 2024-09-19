using BlogaatAPI.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogaatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {



        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<IdentityUser> userManager,
                                     SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _config = config;
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var userrole = await userManager.AddToRoleAsync(user, "User");
                    return Ok("Account Added successfully");
                }

                foreach (var error in result.Errors)
                {
                        ModelState.AddModelError("", error.Description);
                }
                
            }

            return BadRequest(ModelState);
        }






        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var roles = await userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _config["JWT:ValidIssuer"],
                        audience: _config["JWT:ValidAudience"],
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creds
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }

                return Unauthorized();
            }

            return BadRequest(ModelState);
       
        
        
        }






        // GET: api/user
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            // الحصول على جميع المستخدمين
            var users = userManager.Users.ToList();

            // إذا كانت القائمة فارغة
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            // تجهيز البيانات للعرض
            var userList = users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email,
                
            }).ToList();

            return Ok(userList);
        }



    }
}

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // البحث عن المستخدم باستخدام اسم المستخدم
        //        var user = await userManager.FindByNameAsync(model.UserName);
        //        if (user != null)
        //        {
        //            // التحقق من كلمة المرور
        //            bool passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
        //            if (passwordValid)
        //            {
        //                // إنشاء claims للتوكن مثل اسم المستخدم، البريد الإلكتروني، و ID
        //                var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(ClaimTypes.NameIdentifier, user.Id),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };

        //                // الحصول على الأدوار وإضافتها إلى claims
        //                var roles = await userManager.GetRolesAsync(user);
        //                foreach (var role in roles)
        //                {
        //                    claims.Add(new Claim(ClaimTypes.Role, role));
        //                }

        //                // إعداد مفتاح التشفير والبيانات
        //                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
        //                var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //                // إنشاء التوكن
        //                var token = new JwtSecurityToken
        //                (
        //                    issuer: config["JWT:ValidIssuer"],
        //                    audience: config["JWT:ValidAudience"],
        //                    claims: claims,
        //                    expires: DateTime.Now.AddDays(2),
        //                    signingCredentials: signinCredentials
        //                );

        //                // إرجاع التوكن
        //                return Ok(new
        //                {
        //                    token = new JwtSecurityTokenHandler().WriteToken(token),
        //                    expiration = token.ValidTo
        //                });
        //            }

        //            // كلمة المرور غير صحيحة
        //            return Unauthorized(new { Message = "Invalid username or password." });
        //        }

        //        // اسم المستخدم غير موجود
        //        return NotFound(new { Message = "User not found." });
        //    }

        //    // البيانات المدخلة غير صالحة
        //    return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        //}








        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}







