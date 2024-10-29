using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Insfrastructure.Repository
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected AppDbContext DB { get; set; }

		public RepositoryBase(AppDbContext _db)
		{
			DB = _db;
		}

		public IQueryable<T> FindAll()
		{
			return DB.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return DB.Set<T>().Where(expression).AsNoTracking();
		}

		public void Create(T entity)
		{
			DB.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			DB.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			DB.Set<T>().Remove(entity);
		}
	}
}
