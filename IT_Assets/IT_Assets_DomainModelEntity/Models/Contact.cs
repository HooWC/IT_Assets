using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Assets_DomainModelEntity.Models
{
	public class Contact
	{
		[Key]
		public int id { get; set; }
		public string email { get; set; }
		public string name { get; set; }
		public string message { get; set; }
	}
}
