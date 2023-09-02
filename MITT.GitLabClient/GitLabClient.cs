using MITT.GitLabClient.Models;
using Newtonsoft.Json;
using System.Net;

namespace MITT.GitLabClient;

public interface IGitLabClient
{
    Task<bool> AddIssue(AddIssue addIssue);

    Task<List<Branch>> Branches(string projectId = "47123376");

    Task<List<Issue>> Issues(string assigneeId = "14940892", IssueState state = IssueState.all, string labels = "bug");

    Task<IssuesStatistics> IssuesStatistics(string projectId = "47123376");
}

public class GitLabClient : IGitLabClient
{
    private readonly HttpClient _httpClient;
    private static readonly string PrivateToken = "glpat-TyQzNrWkBYibZXo2dERw";

    public GitLabClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<Branch>> Branches(string projectId = "47123376")
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://gitlab.com/api/v4/projects/{projectId}/repository/branches");
        request.Headers.Add("PRIVATE-TOKEN", PrivateToken);
        var response = await _httpClient.SendAsync(request);

        var content = await response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Branch>>(content);
    }

    public async Task<List<Issue>> Issues(string assigneeId = "14940892", IssueState state = IssueState.all, string labels = "bug")
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://gitlab.com/api/v4/issues?assignee_id={assigneeId}&state={state}&labels={labels}");
        request.Headers.Add("PRIVATE-TOKEN", PrivateToken);
        var response = await _httpClient.SendAsync(request);

        var content = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Issue>>(content);
    }

    public async Task<IssuesStatistics> IssuesStatistics(string projectId = "47123376")
    {
        var url = projectId is not null ? $"https://gitlab.com/api/v4/projects/{projectId}/issues_statistics" : "https://gitlab.com/api/v4/issues_statistics";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("PRIVATE-TOKEN", PrivateToken);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<IssuesStatistics>(content);
    }

    public async Task<bool> AddIssue(AddIssue addIssue)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://gitlab.com/api/v4/projects/{addIssue.ProjectId}/issues");
        request.Headers.Add("PRIVATE-TOKEN", PrivateToken);

        request.Content = new StringContent(JsonConvert.SerializeObject(addIssue));
        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return response.StatusCode > HttpStatusCode.OK && response.StatusCode < HttpStatusCode.IMUsed;
    }
}

public enum IssueState
{
    opened, closed, all
}