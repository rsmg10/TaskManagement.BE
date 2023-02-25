using Microsoft.AspNetCore.Mvc;
using MITT.API.Shared;
using MITT.Services;
using MITT.Services.Abstracts;

namespace MITT.API.Controllers;

public class ProjectsController : BaseController
{
    private readonly IProjectsService _projectService;
    private readonly IManagerService _managerService;

    public ProjectsController(IProjectsService projectsService, IManagerService managerService)
    {
        _projectService = projectsService;
        _managerService = managerService;
    }

    [HttpGet]
    public async Task<List<ProjectVm>> Projects(CancellationToken cancellationToken = default)
        => await _projectService.Projects(cancellationToken);

    [HttpGet]
    public async Task<List<ProjectDto>> ProjectsToAssign(string managerId, CancellationToken cancellationToken = default)
        => await _projectService.ProjectsToAssign(managerId, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> AddOrUpdateProject(ProjectDto projectDto, CancellationToken cancellationToken = default)
        => await _projectService.AddOrUpdateProject(projectDto, cancellationToken);

    [HttpGet]
    public async Task<List<ProjectManagerVm>> Managers(ProjectTypeVm projectType = ProjectTypeVm.All, CancellationToken cancellationToken = default)
        => await _managerService.Managers(projectType, cancellationToken);

    [HttpGet]
    public async Task<ManagersByProjectVm> ManagersByProject(string projectId, CancellationToken cancellationToken = default)
        => await _managerService.Managers(projectId, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> AddOrUpdateManager(ProjectManagerDto projectManagerDto, CancellationToken cancellationToken = default)
        => await _managerService.AddOrUpdateManager(projectManagerDto, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> AssignManagersToProject(AssignManagersDto assignManagersDto, CancellationToken cancellationToken = default)
        => await _managerService.AssignManagersToProject(assignManagersDto, cancellationToken);

    [HttpPost]
    public async Task<OperationResult> ChangeState(string managerId, CancellationToken cancellationToken = default)
        => await _managerService.ChangeState(managerId, cancellationToken);
}