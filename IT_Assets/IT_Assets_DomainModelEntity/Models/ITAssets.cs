using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Assets_DomainModelEntity.Models
{
	public class ITAssets
	{
		[Key]
		public int asset_id { get; set; }
		public string asset_type_code { get; set; }
		public string assetImage { get; set; }
		public string description { get; set; }
		public string other_details { get; set; }
		public Boolean status { get; set; }
	}
}
