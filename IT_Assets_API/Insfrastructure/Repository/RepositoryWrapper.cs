using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Insfrastructure.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{

		private AppDbContext DB;

		public RepositoryWrapper(AppDbContext _db)
		{
			DB = _db;
		}

		private IAdmin _admin;
		private IAssetTypes _assetTypes;
		private IEmployeeAssets _employeeAssets;
		private IEmployees _employees;
		private IITAssetInventory _itAssetInventory;
		private IITAssets _itAssets;
		private IContact _contact;

		public IAdmin Admin
		{
			get
			{
				if (_admin == null)
				{
					_admin = new AdminRepository(DB);
				}
				return _admin;
			}
		}

		public IAssetTypes AssetTypes
		{
			get
			{
				if (_assetTypes == null)
				{
					_assetTypes = new AssetTypesRepository(DB);
				}
				return _assetTypes;
			}
		}

		public IEmployeeAssets EmployeeAssets
		{
			get
			{
				if (_employeeAssets == null)
				{
					_employeeAssets = new EmployeeAssetsRepository(DB);
				}
				return _employeeAssets;
			}
		}

		public IEmployees Employees
		{
			get
			{
				if (_employees == null)
				{
					_employees = new EmployeesRepository(DB);
				}
				return _employees;
			}
		}

		public IITAssetInventory ITAssetInventory
		{
			get
			{
				if (_itAssetInventory == null)
				{
					_itAssetInventory = new ITAssetInventoryRepository(DB);
				}
				return _itAssetInventory;
			}
		}

		public IITAssets ITAssets
		{
			get
			{
				if (_itAssets == null)
				{
					_itAssets = new ITAssetsRepository(DB);
				}
				return _itAssets;
			}
		}

		public IContact Contact
		{
			get
			{
				if (_contact == null)
				{
					_contact = new ContactRepository(DB);
				}
				return _contact;
			}
		}

		public void Save()
		{
			DB.SaveChanges();
		}
	}
}
