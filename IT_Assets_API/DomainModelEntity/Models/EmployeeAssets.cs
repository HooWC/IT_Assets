using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelEntity.Models
{
	public class EmployeeAssets
	{
		[Key]
		public int id { get; set; }
		public int asset_id { get; set; }
		public int employee_id { get; set; }
		public DateTime date_out { get; set; }
		public DateTime date_returned { get; set; }
		public string? condition_out { get; set; }
		public string? condition_returned { get; set; }
		public string? other_details { get; set; }
		public Boolean status { get; set; }
		public string? leased { get; set; }
		public int admin { get; set; }
	}
}
