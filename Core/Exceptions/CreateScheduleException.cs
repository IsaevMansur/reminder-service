using System;
using Core.Enums;

namespace Core.Exceptions;

public class CreateScheduleException(string message)
    : ScheduleException(message)
{
    public static void ThrowIfInvalid(DateTime dueDate, ScheduleKind kind, string? message = null)
    {
        SingleKindHasNotExistsTime(dueDate, kind, message);
        NonUtc(dueDate, message);
        InPast(dueDate, message);
    }

    private static void InPast(DateTime dueDate, string? message)
    {
        message ??= "Due date is in the past.";
        if (dueDate > DateTime.UtcNow)
            throw new CreateScheduleException(message);
    }

    private static void NonUtc(DateTime dueDate, string? message)
    {
        message ??= "Due date is non-UTC.";
        if (dueDate.Kind != DateTimeKind.Utc)
            throw new CreateScheduleException(message);
    }

    private static void SingleKindHasNotExistsTime(DateTime dueDate, ScheduleKind kind,
        string? message = null)
    {
        if (kind is not ScheduleKind.Single)
            return;
        message ??= "The due date doesn't have the time when it should.";
        if (dueDate.TimeOfDay == TimeSpan.Zero)
            throw new CreateScheduleException(message);
    }
}