using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Features.Users.Models;

public class UserDTO
{
    public UserDTO()
    {
        RolesDTO = new HashSet<RoleDTO>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? FatherName { get; set; }
    public string IIN { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public UserProfileDTO UserProfileDTO { get; set; }
    public ICollection<RoleDTO> RolesDTO { get; set; }
}
