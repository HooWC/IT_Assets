using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Assets_DomainModelEntity.Models
{
	public class AssetTypes
	{
		[Key]
		public int id { get; set; }
		public string asset_type_code { get; set; }
		public string asset_type_description { get; set; }
	}
}
