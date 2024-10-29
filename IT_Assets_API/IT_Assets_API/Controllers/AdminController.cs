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
	public class AdminController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public AdminController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Admin> GetAllAdmin()
		{
			return _repoWrapper.Admin.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Admin>> GetAdmin(int id)
		{
			var account = await _repoWrapper.Admin.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutAdmin(int id, Admin ad)
		{
			if (id != ad.id)
			{
				return BadRequest();
			}

			_repoWrapper.Admin.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AdminExists(id))
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
		public ActionResult<Admin> PostAdmin(Admin ad)
		{
			_repoWrapper.Admin.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetAdmin", new { id = ad.id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAdmin(int id)
		{
			var ad = await _repoWrapper.Admin.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.Admin.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool AdminExists(int id)
		{
			return _repoWrapper.Admin.FindByCondition(e => e.id == id).Any(e => e.id == id);
		}
	}
}
