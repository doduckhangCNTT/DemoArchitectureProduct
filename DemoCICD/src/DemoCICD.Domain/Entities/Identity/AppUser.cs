using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Entities.Identity;
public class AppUser : IdentityUser<Guid> // Thêm để xác định khóa chính bảng User là Guid
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }

    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Có là giám đốc
    /// </summary>
    public bool IsDirector { get; set; }

    /// <summary>
    /// Có là trưởng phòng
    /// </summary>
    public bool IsHeadOfDepartment { get; set; }

    /// <summary>
    /// Quản lí (chứa id những người có quyền)
    /// </summary>
    public Guid? ManagerId { get; set; }

    public Guid PositionId { get; set; }

    public int IsReceipient { get; set; }

    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
}
