using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NdbPortal.API.Models;
using NdbPortal.Entities.Dtos.Login;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Dtos.Employee;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NdbPortal.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<JwtOptions> _jwtOptionsMonitor;

        public AuthController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<AuthController> logger, IOptionsMonitor<JwtOptions> jwtOptionsMonitor)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtOptionsMonitor = jwtOptionsMonitor;
        }

        [Route("/api/authenticate")]
        [HttpPost]
        public async Task<LoginInfoResponse> Authenticate(LoginInfo loginInfo)
        {
            var employees = await _repository.Employee.GetAllEmployeesAsync();
            var employeesResult = _mapper.Map<IEnumerable<EmployeeGetDto>>(employees).FirstOrDefault(e => e.Email.Equals(loginInfo.Email) && e.Password.Equals(loginInfo.Password));

            var result = new LoginInfoResponse();

            if (employeesResult != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _jwtOptionsMonitor.CurrentValue.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", employeesResult.Id.ToString()),
                    new Claim("Email", employeesResult.Email),
                    new Claim("Password", employeesResult.Password)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionsMonitor.CurrentValue.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _jwtOptionsMonitor.CurrentValue.Issuer, 
                    _jwtOptionsMonitor.CurrentValue.Audience, 
                    claims, 
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn);

                result.Code = 0;
                result.EmployeeId = employeesResult.Id;
                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                result.Code = 1;
            }

            return result;
        }


        [HttpPost]
        [Route("api/validate-token")]
        public ActionResult<Guid> ValidateToken([FromBody] TokenRequest tokenRequest)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtOptionsMonitor.CurrentValue.Key);
                tokenHandler.ValidateToken(tokenRequest.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var employeeId = new Guid(jwtToken.Claims.First(x => x.Type == "Id").Value);

                return Ok(employeeId);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
