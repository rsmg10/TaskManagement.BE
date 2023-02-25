using MITT.EmployeeDb.Models;
using MITT.Services.Helpers.JwtHelper;

namespace MITT.Services.Abstracts;

public interface IAuthService
{
    Task<OperationResult> ChangePin(ChangePinDto changePinDto, CancellationToken cancellationToken);

    Task<OperationResult<JwtToken>> SignIn(SignInDto signInDto, CancellationToken cancellationToken);

    Task<OperationResult> ResetPin(string phone, CancellationToken cancellationToken);
}

public class SignInDto
{
    public string Phone { get; set; }
    public string Pin { get; set; }
}

public class ChangePinDto : SignInDto
{
    public string NewPin { get; set; }
}