using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IRepositoryWrapper
	{
		IAdmin Admin { get; }
		IAssetTypes AssetTypes { get; }
		IEmployeeAssets EmployeeAssets { get; }
		IEmployees Employees { get; }
		IITAssetInventory ITAssetInventory { get; }
		IITAssets ITAssets { get; }
		IContact Contact { get; }
		void Save();
	}
}
