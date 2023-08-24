using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Features.Users.Models;

public class RoleDTO
{
    public int Id { get; set; }
    public UserRole UserRoleCode { get; set; }
    public string RoleName { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDTO UserDTO { get; set; }
}
