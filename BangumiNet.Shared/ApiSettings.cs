using System.Reflection;

namespace BangumiNet.Shared;

public class ApiSettings
{
    public string UserAgent { get; set; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string AuthToken { get; set; } = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BangumiDevToken");
}
