using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Assets_DomainModelEntity.Models
{
	public class Employees
	{
		[Key]
		public int employee_id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string department { get; set; }
		public string extension { get; set; }
		public string email_address { get; set; }
		public string other_details { get; set; }
		public string avatar { get; set; }
		public string position { get; set; }
		public string username { get; set; }
		public string password { get; set; }
	}
}
