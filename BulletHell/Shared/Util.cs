using System;

public static class Util
{
    public static string FormatSeconds(double seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);

        string formatted = "";

        if (t.Hours != 0)
        {
            formatted += $"{t.Hours}h ";
        }

        if (t.Minutes != 0)
        {
            formatted += $"{t.Minutes}m ";
        }

        return $"{formatted}{t.Seconds}s";
    }
}
