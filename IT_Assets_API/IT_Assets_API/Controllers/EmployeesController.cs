using Contracts;
using DomainModelEntity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_Assets_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class EmployeesController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public EmployeesController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Employees> GetAllEmployees()
		{
			return _repoWrapper.Employees.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Employees>> GetEmployees(int id)
		{
			var account = await _repoWrapper.Employees.FindByCondition(e => e.employee_id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutEmployees(int id, Employees ad)
		{
			if (id != ad.employee_id)
			{
				return BadRequest();
			}

			_repoWrapper.Employees.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EmployeesExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		[HttpPost]
		public ActionResult<Employees> PostEmployees(Employees ad)
		{
			_repoWrapper.Employees.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetEmployees", new { id = ad.employee_id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEmployees(int id)
		{
			var emp = await _repoWrapper.Employees.FindByCondition(e => e.employee_id == id).FirstOrDefaultAsync();
			if (emp == null)
			{
				return NotFound();
			}
			_repoWrapper.Employees.Delete(emp);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool EmployeesExists(int id)
		{
			return _repoWrapper.Employees.FindByCondition(e => e.employee_id == id).Any(e => e.employee_id == id);
		}
	}
}
