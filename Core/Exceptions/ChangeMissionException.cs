using Core.Utils;

namespace Core.Exceptions;

public class ChangeMissionException(string? message) : MissionException(message)
{
    public static void ThrowIfInvalidTitle(string title)
    {
        if (!MissionValidator.IsCorrectTitle(title, out string? error))
            throw new ChangeMissionException(error);
    }

    public static void ThrowIfInvalidDescription(string? description)
    {
        if (!MissionValidator.IsCorrectDescription(description, out string? error))
            throw new ChangeMissionException(error);
    }
}