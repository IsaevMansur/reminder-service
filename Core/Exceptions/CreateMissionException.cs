using System;
using Core.Enums;
using Core.Utils;

namespace Core.Exceptions;

public class CreateMissionException(string? message)
    : MissionException(message)
{
    public static void ThrowIfInvalid(string title, DateTime dueDate, MissionKind kind,
        string? description = null,
        string? message = null)
    {
        if (!MissionValidator.IsValid(title, description, dueDate, kind, out string? error))
            throw new CreateMissionException(message ?? error);
    }
}