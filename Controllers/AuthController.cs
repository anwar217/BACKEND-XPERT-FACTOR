using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using factoring1.Services;
using System.Runtime.ConstrainedExecution;


namespace factoring1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly FactoringDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly SmsService _smsService;

        public AuthController(AuthService authService, FactoringDbContext context, IMemoryCache cache, SmsService smsService)
        {
            _authService = authService;
            _context = context;
            _cache = cache;
            _smsService = smsService;
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequestModel model)
           
        {
            var PhoneNumber = model.PhoneNumber.Substring(model.PhoneNumber.Length - 8);
            var universalPhoneNumber1= "+216"+PhoneNumber;
            var universalPhoneNumber2="00216"+PhoneNumber;
            var user = await _context.Individus.FirstOrDefaultAsync(u => u.NumberPhone == PhoneNumber || u.NumberPhone== universalPhoneNumber1 ||u.NumberPhone== universalPhoneNumber2);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var code = GenerateResetCode();
            var cacheKey = $"ResetCode_{user.NumberPhone}";

            _cache.Set(cacheKey, new { Code = code, ExpirationDate = DateTime.UtcNow.AddMinutes(10) }, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            PhoneNumber = "+216" + PhoneNumber;
           await _smsService.SendSmsAsync(PhoneNumber, $"Your password reset code is {code}");

            return Ok(new { message = "Password reset code has been sent to your phone" ,PhoneNumber});
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var cacheKey = $"ResetCode_{model.PhoneNumber}";
            if (!_cache.TryGetValue(cacheKey, out var cachedCode))
            {
                return BadRequest(new { message = "Invalid or expired code" });
            }

            var codeData = (dynamic)cachedCode;
            if (model.Code != codeData.Code || DateTime.UtcNow > codeData.ExpirationDate)
            {
                return BadRequest(new { message = "Invalid or expired code" });
            }

            var user = await _context.Individus.FirstOrDefaultAsync(u => u.NumberPhone == model.PhoneNumber );

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.Password = HashPassword(model.NewPassword);
            await _context.SaveChangesAsync();

            _cache.Remove(cacheKey); // Supprimer le code du cache après utilisation

            return Ok(new { message = "Password has been reset successfully" });
        }

        private string GenerateResetCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // Génère un code à 6 chiffres
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Individu
            {
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                Password = HashPassword(model.Password),

                NumberPhone = model.NumberPhone

            };

            await _context.Individus.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }

            var user = await _context.Individus.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || !VerifyPassword(model.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { message = "Login successful", token ,user});
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult GetProtectedData()
        {
            return Ok("This is protected data.");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }

    public class RegisterModel
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NumberPhone { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordModel
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }

    public class PasswordResetRequestModel
    {
        public string PhoneNumber { get; set; }
    }
}
