using System.ComponentModel.DataAnnotations;

namespace DemoCICD.Persistence.DependenceInjection.Options;
public record SqlServerRetryOptions
{
    [Required, Range(0, 20)]
    public int MaxRetryCount { get; set; }

    [Required, Timestamp]
    public TimeSpan MaxRetryDelay { get; set; }

    public int[]? ErrorNumbersToAdd { get; set; }
}
