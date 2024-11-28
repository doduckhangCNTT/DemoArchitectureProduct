using DemoCICD.Domain.Abstractions.Entities;
using System.Linq.Expressions;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Persistence.Repositories;
public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : DomainEntity<TKey>
{

    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
        => _context = context;

    public void Dispose()
        => _context?.Dispose();

    /// <summary>
    /// Tìm tất cả.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    /// <summary>
    /// Tìm theo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);

    /// <summary>
    /// Thêm mới.
    /// </summary>
    /// <param name="entity">Thực thể.</param>
    public void Add(TEntity entity)
        => _context.Add(entity);

    /// <summary>
    /// Xóa thực thể.
    /// </summary>
    /// <param name="entity">Thực thể muốn xóa.</param>
    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

    /// <summary>
    /// Xóa nhiều thực thể.
    /// </summary>
    /// <param name="entities">Danh sách các thực thể muốn xóa.</param>
    public void RemoveMultiple(List<TEntity> entities)
        => _context.Set<TEntity>().RemoveRange(entities);

    /// <summary>
    /// Cập nhật thông tin thực thể.
    /// </summary>
    /// <param name="entity">Thực thể cần cập nhật.</param>
    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);
}
