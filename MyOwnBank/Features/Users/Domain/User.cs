namespace MyOwnBank.Features.Users.Domain;

public class User
{
    public User() 
    {
        Roles = new HashSet<Role>();
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
    public UserProfile UserProfile { get; set; }
    public ICollection<Role> Roles { get; set; }
}
