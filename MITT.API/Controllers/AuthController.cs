using Microsoft.AspNetCore.Mvc;
using MITT.API.Shared;
using MITT.Services.Abstracts;
using MITT.Services.Helpers.JwtHelper;

namespace MITT.API.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost]
    public async Task<OperationResult<JwtToken>> SignIn(SignInDto signInDto, CancellationToken cancellationToken)
        => await _authService.SignIn(signInDto, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> ChangePin(ChangePinDto changePinDto, CancellationToken cancellationToken)
        => await _authService.ChangePin(changePinDto, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> ResetPin(string phone, CancellationToken cancellationToken)
        => await _authService.ResetPin(phone, cancellationToken);
}