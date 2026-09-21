using APSEstate.CONCRETE.Interface;
using APSEstate.CONCRETE.JWTService;
using APSEstate.CORE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APSEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository,IConfiguration configuration)
        {
            _authRepository = authRepository;
            _config = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel data)
        {
            try
            {
                var result = await _authRepository.LoginAsync(data);

                if (result.success && result.data != null)
                {
                    var loginUser = result.data;

                    var token = JwtTokenService.GenerateToken(loginUser,_config);

                    return Ok(new
                    {
                        success = true,
                        message = result.message,
                        token = token,
                        user = result.data
                    });
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
