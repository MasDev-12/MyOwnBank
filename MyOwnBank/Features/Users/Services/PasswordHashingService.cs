using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using MyOwnBank.Features.Users.Domain;
using MyOwnBank.Features.Users.Options;
using System.Security.Cryptography;
using System.Text;

namespace MyOwnBank.Features.Users.Services;

public class PasswordHashingService
{
    private readonly Argon2Options _argon2Options;

    public PasswordHashingService(IOptions<Argon2Options> argon2Options)
    {
        _argon2Options = argon2Options.Value;
    }

    public string GetPasswordHash(string password) 
    {
        byte[] Salt = GenerateSalt();
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)) 
        {
             DegreeOfParallelism = _argon2Options.DegreeOfParallelism,
             Iterations = _argon2Options.Iterations,
             MemorySize = _argon2Options.MemorySize,
             Salt = Salt
        };

        byte[] passwordHash = argon2.GetBytes(_argon2Options.HashLengthInBytes);
        var passwordHashAndSatl = $"{Convert.ToBase64String(passwordHash)}:{Convert.ToBase64String(Salt)}:{_argon2Options.DegreeOfParallelism}:{_argon2Options.Iterations}:{_argon2Options.MemorySize}";
        return passwordHashAndSatl;
    }

    public string CheckPasswordHash(User user, string password)
    {
        string[] userPasswordHashOptions = user.Password.Split(':');
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = int.Parse(userPasswordHashOptions[2]),
            Iterations = int.Parse(userPasswordHashOptions[3]),
            MemorySize = int.Parse(userPasswordHashOptions[4]),
            Salt = Encoding.UTF8.GetBytes(userPasswordHashOptions[1]),
        };
        string passwordHash = Convert.ToBase64String(argon2.GetBytes(_argon2Options.HashLengthInBytes));
        return passwordHash;
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[_argon2Options.HashLengthInBytes];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
}
