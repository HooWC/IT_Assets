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
	public class AssetTypesController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public AssetTypesController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<AssetTypes> GetAllAssetTypes()
		{
			return _repoWrapper.AssetTypes.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AssetTypes>> GetAssetTypes(int id)
		{
			var account = await _repoWrapper.AssetTypes.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutAssetTypes(int id, AssetTypes ad)
		{
			if (id != ad.id)
			{
				return BadRequest();
			}

			_repoWrapper.AssetTypes.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AssetTypesExists(id))
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
		public ActionResult<AssetTypes> PostAssetTypes(AssetTypes ad)
		{
			_repoWrapper.AssetTypes.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetAssetTypes", new { id = ad.id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAssetTypes(int id)
		{
			var ad = await _repoWrapper.AssetTypes.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.AssetTypes.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool AssetTypesExists(int id)
		{
			return _repoWrapper.AssetTypes.FindByCondition(e => e.id == id).Any(e => e.id == id);
		}
	}
}
