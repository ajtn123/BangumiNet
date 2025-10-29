using BangumiNet.Api.Interfaces;

namespace BangumiNet.Models;

internal class WeekdayNames(DayOfWeek day) : IWeekday
{
    public DayOfWeek DayOfWeek { get; set; } = day;

    public string? Cn => DayOfWeek switch
    {
        DayOfWeek.Sunday => "星期天",
        DayOfWeek.Monday => "星期一",
        DayOfWeek.Tuesday => "星期二",
        DayOfWeek.Wednesday => "星期三",
        DayOfWeek.Thursday => "星期四",
        DayOfWeek.Friday => "星期五",
        DayOfWeek.Saturday => "星期六",
        _ => throw new NotImplementedException(),
    };
    public string? En => DayOfWeek.ToString();
    public string? Ja => DayOfWeek switch
    {
        DayOfWeek.Sunday => "日曜日",
        DayOfWeek.Monday => "月曜日",
        DayOfWeek.Tuesday => "火曜日",
        DayOfWeek.Wednesday => "水曜日",
        DayOfWeek.Thursday => "木曜日",
        DayOfWeek.Friday => "金曜日",
        DayOfWeek.Saturday => "土曜日",
        _ => throw new NotImplementedException(),
    };
    public int? Id => DayOfWeek.ToInt(startingIndex: 1);
}
