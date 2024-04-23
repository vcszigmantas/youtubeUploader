using DataAccess.Entities;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
	public interface IRepository<TEntity>
		where TEntity : BaseEntity
	{
		Task<List<TEntity>> ListAsync(
			int? limit,
			Expression<Func<TEntity, bool>>? filter = null
		);

		Task<TEntity> CreateAsync(TEntity entity);

		Task<TEntity?> GetAsync(int id);

		Task<TEntity> UpdateAsync(TEntity entity);

		Task<bool> DeleteAsync(int id);
	}
}
