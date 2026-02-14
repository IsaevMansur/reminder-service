using Core.Utils;

namespace Core.Exceptions;

public class CreateMissionException(string? message, string? paramName = null)
    : MissionException(message)
{
    public static void ThrowIfInvalid(string title, string? description = null,
        string? message = null)
    {
        if (!MissionValidator.IsValid(title, description, out string? error))
            throw new CreateMissionException(message ?? error);
    }
}