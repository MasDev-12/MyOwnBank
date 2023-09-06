namespace MyOwnBank.Features.Authentification.Options;

public class RefreshTokenOptions
{
    public TimeSpan StoragePeriod { get; set; }
    public TimeSpan ValidityPeriod { get; set; }
    public int LengthBytes { get; set; }
}
