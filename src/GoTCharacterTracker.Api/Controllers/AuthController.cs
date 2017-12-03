using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace GoTCharacterTracker.Api.Controllers
{
    [Authorize]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IConfiguration m_config;
        private string m_user;
        private string m_pwd;
        private string m_securityKey;

        public AuthController(IConfiguration config) {
            m_config = config;
            m_user = m_config["MasterLoginUsername"];
            m_pwd = m_config["MasterLoginPassword"];
            m_securityKey = m_config["JWTSecurityKey"];


        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequestDTO request)
        {
            // TODO: Drop in Identity for user management
            if (request.Username == m_user && request.Password == m_pwd)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_securityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return BadRequest("Could not verify identity");
        }

    }

    public class TokenRequestDTO {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
