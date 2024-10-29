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
	public class ContactController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public ContactController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Contact> GetAllContact()
		{
			return _repoWrapper.Contact.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Contact>> GetContact(int id)
		{
			var account = await _repoWrapper.Contact.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutContact(int id, Contact ad)
		{
			if (id != ad.id)
			{
				return BadRequest();
			}

			_repoWrapper.Contact.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ContactExists(id))
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
		public ActionResult<Contact> PostContact(Contact ad)
		{
			_repoWrapper.Contact.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetContact", new { id = ad.id }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteContact(int id)
		{
			var ad = await _repoWrapper.Contact.FindByCondition(e => e.id == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.Contact.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool ContactExists(int id)
		{
			return _repoWrapper.Contact.FindByCondition(e => e.id == id).Any(e => e.id == id);
		}
	}
}
