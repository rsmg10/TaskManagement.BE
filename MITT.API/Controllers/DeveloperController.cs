using Microsoft.AspNetCore.Mvc;
using MITT.API.Shared;
using MITT.Services.Abstracts;

namespace MITT.API.Controllers;

public class DeveloperController : BaseController
{
    private readonly IDeveloperService _developerService;

    public DeveloperController(IDeveloperService developerService) => _developerService = developerService;

    [HttpGet]
    public async Task<List<DeveloperVm>> Developers(bool activeOnly = true, CancellationToken cancellationToken = default)
        => await _developerService.Developers(activeOnly, cancellationToken);

    [HttpGet]
    public async Task<List<DeveloperVm>> DevelopersByTask(string taskId, CancellationToken cancellationToken = default)
        => await _developerService.Developers(taskId, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> AddOrUpdateDeveloper(DeveloperDto developer, CancellationToken cancellationToken = default)
        => await _developerService.AddOrUpdateDeveloper(developer, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> ChangeState(string developerId, CancellationToken cancellationToken = default)
        => await _developerService.ChangeState(developerId, cancellationToken);
}