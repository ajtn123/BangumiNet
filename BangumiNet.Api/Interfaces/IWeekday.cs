namespace BangumiNet.Api.Interfaces;

public interface IWeekday
{
    /// <summary>中文名</summary>
    string? Cn { get; set; }
    /// <summary>英文名</summary>
    string? En { get; set; }
    /// <summary>日语</summary>
    string? Ja { get; set; }
    /// <summary>从1与星期一开始的序号</summary>
    int? Id { get; set; }
}
