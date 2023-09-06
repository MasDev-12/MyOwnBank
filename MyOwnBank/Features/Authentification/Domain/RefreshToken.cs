using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Features.Authentification.Domain;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime TokenValidityPeriod { get; set; }
    public DateTime TokenStoragePeriod { get; set; }
    public bool Revoked { get; set; }
    public int? ReplacedByNextToken { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
