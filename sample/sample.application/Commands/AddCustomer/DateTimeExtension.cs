namespace sample.application.Commands.AddCustomer;

public static class DateTimeExtension {
    public static long ToEpoch(this DateTime date)
    {
        TimeSpan t = DateTime.Now - new DateTime(date.Year, date.Month, date.Day);
        int secondsSinceEpoch = (int)t.TotalSeconds;
        return secondsSinceEpoch;
    }
}