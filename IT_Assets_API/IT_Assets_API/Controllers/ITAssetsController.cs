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
	public class ITAssetsController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public ITAssetsController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<ITAssets> GetAllITAssets()
		{
			return _repoWrapper.ITAssets.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ITAssets>> GetITAssets(int id)
		{
			var account = await _repoWrapper.ITAssets.FindByCondition(e => e.asset_id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutITAssets(int id, ITAssets ad)
		{
			if (id != ad.asset_id)
			{
				return BadRequest();
			}

			_repoWrapper.ITAssets.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ITAssetsExists(id))
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
		public ActionResult<ITAssets> PostITAssets(ITAssets ad)
		{
			_repoWrapper.ITAssets.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetITAssets", new { id = ad.asset_id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteITAssets(int id)
		{
			var ad = await _repoWrapper.ITAssets.FindByCondition(e => e.asset_id == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.ITAssets.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool ITAssetsExists(int id)
		{
			return _repoWrapper.ITAssets.FindByCondition(e => e.asset_id == id).Any(e => e.asset_id == id);
		}
	}
}
