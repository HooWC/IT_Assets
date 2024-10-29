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
	public class EmployeeAssetsController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public EmployeeAssetsController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<EmployeeAssets> GetAllEmployeeAssets()
		{
			return _repoWrapper.EmployeeAssets.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<EmployeeAssets>> GetEmployeeAssets(int id)
		{
			var account = await _repoWrapper.EmployeeAssets.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutEmployeeAssets(int id, EmployeeAssets ad)
		{
			if (id != ad.id)
			{
				return BadRequest();
			}

			_repoWrapper.EmployeeAssets.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EmployeeAssetsExists(id))
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
		public ActionResult<EmployeeAssets> PostEmployeeAssets(EmployeeAssets ad)
		{
			_repoWrapper.EmployeeAssets.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetEmployeeAssets", new { id = ad.id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEmployeeAssets(int id)
		{
			var ad = await _repoWrapper.EmployeeAssets.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.EmployeeAssets.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool EmployeeAssetsExists(int id)
		{
			return _repoWrapper.EmployeeAssets.FindByCondition(e => e.id == id).Any(e => e.id == id);
		}
	}
}
