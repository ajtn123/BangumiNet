namespace BangumiNet.Api.Interfaces;

public interface IRatingCount
{
    /// <summary>1 分</summary>
    int? One { get; set; }
    /// <summary>2 分</summary>
    int? Two { get; set; }
    /// <summary>3 分</summary>
    int? Three { get; set; }
    /// <summary>4 分</summary>
    int? Four { get; set; }
    /// <summary>5 分</summary>
    int? Five { get; set; }
    /// <summary>6 分</summary>
    int? Six { get; set; }
    /// <summary>7 分</summary>
    int? Seven { get; set; }
    /// <summary>8 分</summary>
    int? Eight { get; set; }
    /// <summary>9 分</summary>
    int? Nine { get; set; }
    /// <summary>10 分</summary>
    int? OneZero { get; set; }

    List<int> ToList() => [
        One ?? 0,
        Two ?? 0,
        Three ?? 0,
        Four ?? 0,
        Five ?? 0,
        Six ?? 0,
        Seven ?? 0,
        Eight ?? 0,
        Nine ?? 0,
        OneZero ?? 0
    ];
}
