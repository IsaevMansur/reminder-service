using Core.Models;

namespace Core.Utils;

public static class MissionValidator
{
    public static bool IsValid(string title, string? description, out string? error)
    {
        return IsCorrectTitle(title, out error)
               && IsCorrectDescription(description, out error);
    }

    public static bool IsCorrectTitle(string title, out string? error)
    {
        error = null;
        if (string.IsNullOrWhiteSpace(title))
            error = "Title cannot be null or whitespace.";
        error = title.Length switch
        {
            > Mission.TitleMaxLength =>
                $"Title too long. Title must be less than {Mission.TitleMaxLength} characters.",
            < Mission.TitleMinLength =>
                $"Title is too short. Title must be greater than {Mission.TitleMinLength} characters.",
            _ => null
        };
        return error is null;
    }

    public static bool IsCorrectDescription(string? description, out string? error)
    {
        error = null;
        if (description is null) return false;
        if (description.Length > Mission.DescriptionMaxLength)
            error =
                $"Description too long. Description must be less than {Mission.DescriptionMaxLength}.";
        return error is null;
    }
}