using backend.Dtos.Account;
using backend.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<IdentityUser> userManager, ITokenService tokenService)

        {

            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)

        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // 1. Create a New Microsoft User
                var appUser = new IdentityUser
                {

                    UserName = registerDto.username,
                    Email = registerDto.Email
                };

                // 2. let microsoft securly hash the password and save the user
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    // 3. Return our DTO containing the newly generated JWT Token

                    return Ok(new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    return StatusCode(500,createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}