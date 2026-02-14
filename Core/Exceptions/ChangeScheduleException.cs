using System;
using Core.Enums;
using Core.Utils;

namespace Core.Exceptions;

public class ChangeScheduleException(string? message)
    : ScheduleException(message)
{
    public static void ThrowIfCannotReschedule(DateTime dueDate, ScheduleKind kind,
        string? message = null)
    {
        if (!ScheduleValidator.IsValid(dueDate, kind, out string? error))
            throw new ChangeScheduleException(message ?? error);
    }
}