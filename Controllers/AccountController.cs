using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BiteBox.Backend.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("[action]")]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration; // Aggiungi questo campo
    private readonly ApplicationDbContext _context;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }
    
    
    [HttpGet]
    public IActionResult GoogleLogin()
    {
        // L'oggetto AuthenticationProperties permette di specificare il RedirectUri dopo il login con Google
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleCallback", "Account")
        };

        // Reindirizza l'utente alla pagina di login di Google
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("signin-google")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;
        
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = name,
                Email = email
            };

            var identityResult = await _userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
        }
        
        await _signInManager.SignInAsync(user, isPersistent: false);

        // A questo punto puoi generare un token JWT o usare i cookie di Identity
        return Ok(new { message = "Login riuscito", user });
    }
}