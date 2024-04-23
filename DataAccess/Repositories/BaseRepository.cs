﻿using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
	public class BaseRepository<TEntity> : IRepository<TEntity>
		where TEntity : BaseEntity
	{
		private readonly YoutubeAppDbContext _context;
		private readonly DbSet<TEntity> _entities;

		public BaseRepository(YoutubeAppDbContext context)
		{
			_context = context;
			_entities = _context.Set<TEntity>();
		}

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			await _entities.AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			TEntity? entity = await GetAsync(id);

			if (entity == null)
			{
				return false;
			}

			_entities.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<TEntity?> GetAsync(int id)
		{
			return await _entities.FindAsync(id);
		}

		public async Task<List<TEntity>> ListAsync(
			int? limit, 
			Expression<Func<TEntity, bool>>? filter = null)
		{
			IQueryable<TEntity> query = _entities;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			query = query.OrderByDescending(x => x.Id);

			if (limit.HasValue)
			{
				query = query.Take(limit.Value);
			}

			return await query.ToListAsync();
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			_entities.Update(entity);
			await _context.SaveChangesAsync();

			return entity;
		}
	}
}
