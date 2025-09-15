using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BangumiNet.Utils;

public static class Common
{
    /// <summary>
    /// Parse date string to <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="date"><c>yyyy-MM-DD</c></param>
    /// <returns></returns>
    public static DateOnly? ParseDate(string? date)
    {
        if (DateOnly.TryParseExact(date, "", out DateOnly result))
            return result;
        else return null;
    }
}
