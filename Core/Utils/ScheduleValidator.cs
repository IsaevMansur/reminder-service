using System;
using Core.Enums;

namespace Core.Utils;

public static class ScheduleValidator
{
    public static bool IsValid(DateTime dueDate, ScheduleKind kind, out string? error)
    {
        return IsNotInPast(dueDate, out error) && IsUtc(dueDate, out error) &&
               HasTimeIfMust(dueDate, kind, out error);
    }

    private static bool IsNotInPast(DateTime dueDate, out string? error)
    {
        error = null;
        if (dueDate > DateTime.UtcNow)
            error = "Due date is in the past.";
        return error is null;
    }

    private static bool IsUtc(DateTime dueDate, out string? error)
    {
        error = null;
        if (dueDate.Kind != DateTimeKind.Utc)
            error = "Due date isn't in UTC.";
        return error is null;
    }

    private static bool HasTimeIfMust(DateTime dueDate, ScheduleKind kind,
        out string? error)
    {
        error = null;
        if (kind is not ScheduleKind.Single)
            return true;
        if (dueDate.TimeOfDay == TimeSpan.Zero)
            error = "The due date doesn't have the time when it should.";
        return error is null;
    }
}