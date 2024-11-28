namespace DemoCICD.Domain.Abstractions.Entities;

/// <summary>
/// Thực thể chung.
/// </summary>
/// <typeparam name="TKey">Kiểu dữ liệu khóa.</typeparam>
public abstract class DomainEntity<TKey>
{
    /// <summary>
    /// Mã thực thể.
    /// </summary>
    public virtual TKey Id { get; set; }

    /// <summary>
    /// True if domain entity has a identity.
    /// </summary>
    /// <returns>Bool.</returns>
    public bool IsTransient()
    {
        return Id.Equals(default(TKey));
    }
}
