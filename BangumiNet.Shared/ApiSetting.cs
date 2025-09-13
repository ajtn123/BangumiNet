namespace BangumiNet.Shared;

public class ApiSetting
{
    public string UserAgent { get; set; } = "ajtn123/BangumiNet/0.1.0 (Windows) (https://github.com/ajtn123/BangumiNet)";
    public string AuthToken { get; set; } = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BangumiDevToken");
}
