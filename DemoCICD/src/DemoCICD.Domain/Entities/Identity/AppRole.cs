using System.Security;
using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Entities.Identity;
public class AppRole : IdentityRole<Guid>
{
    /// <summary>
    /// Mô tả role
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Mã role
    /// </summary>
    public string RoleCode { get; set; }

    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
    public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
}
