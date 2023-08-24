using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Features.Users.Models;

public class UserProfileDTO
{
    public int Id { get; set; }
    public string CodeWord { get; set; }
    public int Age { get; set; }
    public int UserId { get; set; }
    public UserDTO UserDTO { get; set; }
}
