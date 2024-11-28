using DemoCICD.Domain.Abstractions;

namespace DemoCICD.Persistence;
public class EFUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Đối tượng DB.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Hàm tạo.
    /// </summary>
    /// <param name="context">Đối tượng Db.</param>
    public EFUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync();

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();
}
