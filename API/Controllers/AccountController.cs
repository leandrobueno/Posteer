using System.Text;
using Api.Services;
using API.Models;
using API.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenService _token;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _host;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService token, IMapper mapper, IHostEnvironment host)
    {
      _host = host;
      _mapper = mapper;
      _token = token;
      _signInManager = signInManager;
      _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult<UserToReturn>> Register(UserToRegister register)
    {
      var userExists = await _userManager.FindByEmailAsync(register.Email);
      if (userExists != null) return BadRequest("Email already exists.");

      var user = _mapper.Map<ApplicationUser>(register);
      var result = await _userManager.CreateAsync(user, register.Password);

      if (!result.Succeeded) return BadRequest(result.Errors);

      var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

      if (_host.IsDevelopment()) await VerifyEmail(token, user.Email);

      return CreatedAtRoute("Register", new UserToReturn
      {
        UserName = user.UserName,
        Email = user.Email,
        Token = string.Empty
      });
    }

    [AllowAnonymous]
    [HttpPost("verifyEmail")]
    public async Task<IActionResult> VerifyEmail(string token, string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null) return Unauthorized();

      var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
      var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

      if (!result.Succeeded) return BadRequest("Error veryfing email");

      return Ok("Confirmed");
    }
  }
}