using System.ComponentModel;

namespace MyOwnBank.Features.Users.Domain;

public class Role
{
    public int Id { get; set; }
    public UserRole UserRoleCode { get; set; }
    public string RoleName { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; } 

}

public enum UserRole
{
    [Description("UserRole")]
    UserRole = 0,
    [Description("AnalystRole")]
    AnalystRole = 1,
    [Description("AdministratorRole")]
    AdministratorRole = 2,
}