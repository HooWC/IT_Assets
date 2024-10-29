using Contracts;
using DomainModelEntity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace IT_Assets_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesTokenController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		private IConfiguration _configuration;
		public EmployeesTokenController(IConfiguration configuration, IRepositoryWrapper repoWrapper)
		{
			_configuration = configuration;
			_repoWrapper = repoWrapper;
		}

		[HttpPost]
		public async Task<IActionResult> Post(Employees userInfo)
		{
			if (userInfo != null && userInfo.username != null && userInfo.password != null)
			{
				//Member
				var user = await GetEmployees(userInfo.username, userInfo.password);

				if (user.Value != null)
				{
					var claim = new[] {
					new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
					new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
					new Claim("employee_id",user.Value.employee_id.ToString()),
					new Claim("username",user.Value.username),
					new Claim("password",user.Value.password)
					};
					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claim,
						expires: DateTime.Now.AddMinutes(20),
						signingCredentials: signIn
						);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
				else
				{
					return BadRequest("Invalid credentials");
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		public async Task<ActionResult<Employees>> GetEmployees(string Username, string Password)
		{
			return await _repoWrapper.Employees.FindByCondition(e => e.username == Username && e.password == Password).FirstOrDefaultAsync();
		}
	}
}
