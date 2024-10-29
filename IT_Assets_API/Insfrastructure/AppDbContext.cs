using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModelEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace Insfrastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{
		}

		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Admin> Admin { get; set; } = null!;
		public DbSet<AssetTypes> AssetTypes { get; set; } = null!;
		public DbSet<EmployeeAssets> EmployeeAssets { get; set; } = null!;
		public DbSet<Employees> Employees { get; set; } = null!;
		public DbSet<ITAssetInventory> ITAssetInventory { get; set; } = null!;
		public DbSet<ITAssets> ITAssets { get; set; } = null!;
		public DbSet<Contact> Contact { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=LAPTOP-75SCS0RS\\SQLEXPRESS;Database=IT_Assets;Trusted_Connection=True;TrustServerCertificate=true");
			}
		}

	}
}
