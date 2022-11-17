using System.Security.Claims;
using System.Text;
using Api.Services;
using API.Models.Account;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

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
    private readonly EmailSender _emailSender;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService token, IMapper mapper, IHostEnvironment host, EmailSender emailSender)
    {
      _emailSender = emailSender;
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

      await _signInManager.SignInAsync(user, register.RememberMe);

      var origin = Request.Headers["origin"];
      var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

      var verifyUrl = $"{origin}/confirm/?token={token}&email={user.Email}";
      var messageBody = $"<p>Please click the below link to verify your email address:</p><p><a href='{verifyUrl}'>Verify email</a></p>";

      await _emailSender.SendEmail(user.Email, "Verify Posteer email", messageBody);

      await VerifyEmail(token, user.Email);

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

    [AllowAnonymous]
    [HttpGet("resendEmail")]
    public async Task<IActionResult> ResendEmailConfirmationLink(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);

      if (user == null) return Unauthorized();

      var origin = Request.Headers["origin"];
      var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

      var verifyUrl = $"{origin}/confirm/?token={token}&email={user.Email}";
      var messageBody = $"<p>Please click the below link to verify your email address:</p><p><a href='{verifyUrl}'>Verify email</a></p>";

      await _emailSender.SendEmail(user.Email, "Verify Posteer email", messageBody);

      return Ok("Email sent");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserToReturn>> Login(Login login)
    {
      var user = await _userManager.FindByEmailAsync(login.Email);
      //Validations
      if (user == null) return BadRequest("User not found");
      if (user.Email == "bot@financialchat.com") user.EmailConfirmed = true;
      if (!user.EmailConfirmed) return BadRequest("Email not confirmed");
      if ((await _userManager.CheckPasswordAsync(user, login.Password)) == false)
        return BadRequest("Incorrect email or password");

      var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
      if (!result.Succeeded) return BadRequest("Login failed");

      return Ok(new UserToReturn
      {
        UserName = user.UserName,
        Email = user.Email,
        Token = _token.CreateToken(user.Email, user)
      });
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserToReturn>> CurrentUser()
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
      if (user != null)
      {
        return Ok(new UserToReturn
        {
          UserName = user.UserName,
          Email = user.Email,
          Token = _token.CreateToken(user.Email, user)
        });
      }

      return BadRequest("User not found, login again");
    }
  }
}