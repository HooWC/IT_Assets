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
	public class ITAssetInventoryController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public ITAssetInventoryController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<ITAssetInventory> GetAllITAssetInventory()
		{
			return _repoWrapper.ITAssetInventory.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ITAssetInventory>> GetITAssetInventory(int id)
		{
			var account = await _repoWrapper.ITAssetInventory.FindByCondition(e => e.it_asset_inventory_id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutITAssetInventory(int id, ITAssetInventory ad)
		{
			if (id != ad.it_asset_inventory_id)
			{
				return BadRequest();
			}

			_repoWrapper.ITAssetInventory.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ITAssetInventoryExists(id))
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
		public ActionResult<ITAssetInventory> PostITAssetInventory(ITAssetInventory ad)
		{
			_repoWrapper.ITAssetInventory.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetITAssetInventory", new { id = ad.it_asset_inventory_id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteITAssetInventory(int id)
		{
			var ad = await _repoWrapper.ITAssetInventory.FindByCondition(e => e.it_asset_inventory_id == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.ITAssetInventory.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool ITAssetInventoryExists(int id)
		{
			return _repoWrapper.ITAssetInventory.FindByCondition(e => e.it_asset_inventory_id == id).Any(e => e.it_asset_inventory_id == id);
		}
	}
}
