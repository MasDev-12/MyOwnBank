namespace MyOwnBank.Features.Users.Options;

public class Argon2Options
{
    public int DegreeOfParallelism { get; set; }
    public int HashLengthInBytes { get; set; }
    public int Iterations { get; set; }
    public int MemorySize { get; set; }
}
