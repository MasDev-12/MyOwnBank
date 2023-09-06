using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyOwnBank.Database;
using MyOwnBank.Features.Users.Domain;
using MyOwnBank.Features.Users.Options;
using MyOwnBank.Features.Users.Services;
using System.Text.RegularExpressions;

namespace MyOwnBank.Features.Users.Requests;

public static class Registration
{
    public record Request(
        string Name, string LastName, string FatherName
        ,string Password, string IIN, string PhoneNumber
        , string EmailAddress, string CodeWord, DateTime BirthDate, string EmailPasswordHash):IRequest<Respone>;

    public record Respone(string message);

    public class RequestValidator : AbstractValidator<Request> 
    {
        public RequestValidator(ApplicationDbContext applicationDbContext)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("NameNotEmpty");

            RuleFor(x => x.LastName)
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .WithErrorCode("LastNameNotEmpty");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("PasswordNotEmpty")
                .MinimumLength(6)
                .WithErrorCode("MinLengthMustBeSix");

            RuleFor(x => x.IIN)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("IIN_NotEmpty")
                .Must(BeAllDigits)
                .MustAsync(async (x, token) =>
                {
                    var IINExists = await applicationDbContext.Users.AnyAsync(user => user.IIN == x, token);
                    return !IINExists;
                }).WithErrorCode("IIN already exists");

            RuleFor(x => x.IIN)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("IIN_NotEmpty")
                .MustAsync(async (x, token) =>
                {
                    var IINExists = await applicationDbContext.Users.AnyAsync(user => user.IIN == x, token);
                    return !IINExists;
                }).WithErrorCode("IIN already exists");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("PhoneNumber_notEmpty")
                .Must(BeAllDigits)
                .WithErrorCode("PhoneNumberValid");

            RuleFor(x => x.PhoneNumber)
               .Cascade(CascadeMode.Stop)
               .MustAsync(async (x, token) =>
               {
                   var PhoneNumber = await applicationDbContext.Users.AnyAsync(user => user.PhoneNumber == x, token);

                   return !PhoneNumber;
               }).WithErrorCode("Phone already exists");

            RuleFor(x => x.EmailAddress)
              .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("EmailNotEmpty")
                .EmailAddress()
                .WithErrorCode("InvalidEmailFormat");

            RuleFor(x => x.CodeWord)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("CodeWordNotEmpy");

            RuleFor(x => x.BirthDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("BirthDateNotEmpty")
                .Must(date => IsValidBirthDateMoreThenEightTeen(date))
                .WithErrorCode("AgeMoreTheEightTeen");
        }
        private bool BeAllDigits(string str)
        {
            return str.All(char.IsDigit);
        }

        private bool IsValidBirthDateMoreThenEightTeen(DateTime? birthDate)
        {
            var currentDate = DateTime.UtcNow;
            var minDate = currentDate.AddYears(-18);
            return birthDate <= minDate;
        }

        private bool IsPasswordValid(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }
    }

    public class RegisterRequestHandler : IRequestHandler<Request, Respone>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly PasswordHashingService _passwordHashingService;
        private readonly ILogger<RegisterRequestHandler> _logger;
        private readonly UsersOptions _userOptions;

        public RegisterRequestHandler(ApplicationDbContext applicationDbContext
                                     , IOptions<UsersOptions> userOptions
                                     , PasswordHashingService passwordHashingService
                                     , ILogger<RegisterRequestHandler> logger)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHashingService = passwordHashingService;
            _logger = logger;
            _userOptions = userOptions.Value;
        }

        public async Task<Respone> Handle(Request request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Create new user {request.EmailAddress}");
            UserRole userRole = UserRole.UserRole;
            if (request.EmailAddress.ToLower() == _userOptions.AdministratorEmail)
            {
                userRole = UserRole.AdministratorRole;
            }
            var passwordHashAndSalt = _passwordHashingService.GetPasswordHash(request.Password);
            //TODO need another service to generate Token, to this service need generateConfirm token
            //TODO need emailService to sendEmail, to confirm User
            _logger.LogInformation($"Email no confirm was send to {request.EmailAddress}");
            var user = new User()
            {
                Name = request.Name,
                LastName = request.LastName,
                FatherName = request.FatherName,
                Password = passwordHashAndSalt,
                EmailPasswordHash = request.EmailPasswordHash,
                IIN = request.IIN,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                Confirm = false,
                ConfirmToken = "To Do",
                BirthDate = request.BirthDate,
                CreatedAt = DateTime.UtcNow,
                Roles = new List<Role>
                {
                    new Role()
                    {
                        UserRoleCode = userRole,
                        RoleName = userRole.ToString(),
                        CreatedAt = DateTime.UtcNow
                    }
                }
            };

            await _applicationDbContext.Users.AddAsync(user, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"User was saved in Db, user: {request.EmailAddress}");
            var age = DateTime.UtcNow.Year - request.BirthDate.Year;
            if (DateTime.UtcNow < request.BirthDate.AddYears(age))
            {
                age--;
            }
            var userProfile = new UserProfile()
            {
                UserId = user.Id,
                CodeWord = request.CodeWord,
                Age = age
            };
            await _applicationDbContext.UserProfiles.AddAsync(userProfile, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"User Profile was saved in Db, user: {request.EmailAddress}");
            return new Respone("To confirm your register you need confirm register, please check your email");
        }
    }
}
