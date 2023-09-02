// See https://aka.ms/new-console-template for more information

using MITT.GitLabClient;

IGitLabClient myClient = new GitLabClient(new HttpClient());

var branches = await myClient.Branches();

var issues = await myClient.Issues();

var issuesStatistics = await myClient.IssuesStatistics();

Console.WriteLine($"{branches.Count} folks love work!");