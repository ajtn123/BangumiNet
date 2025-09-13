namespace BangumiNet.Shared;

public class ApiSetting
{
    public string UserAgent { get; set; } = "ajtn123/BangumiNet/1.0";
    public string AuthToken { get; set; } = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BangumiDevToken");
}
