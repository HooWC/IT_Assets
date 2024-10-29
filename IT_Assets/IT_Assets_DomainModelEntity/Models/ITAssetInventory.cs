using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Assets_DomainModelEntity.Models
{
	public class ITAssetInventory
	{
		[Key]
		public int it_asset_inventory_id { get; set; }
		public int asset_id { get; set; }
		public DateTime inventory_date { get; set; }
		public int number_assigned { get; set; }
		public int number_in_stock { get; set; }
		public string other_details { get; set; }
	}
}
