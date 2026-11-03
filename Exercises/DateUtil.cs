namespace BasicExercises.Exercises;

public static class DateUtil
{
    public static bool IsLeapYear(int year)
    {
        if (year < 1 || year > 9999)
            return false;

        return year % 400 == 0 ||
               (year % 4 == 0 && year % 100 != 0);
    }

    public static bool IsValidDate(int day, int month, int year)
    {
        if (year < 1 || year > 9999 ||
            month < 1 || month > 12 ||
            day < 1)
            return false;

        return day <= DateTime.DaysInMonth(year, month);
    }
}