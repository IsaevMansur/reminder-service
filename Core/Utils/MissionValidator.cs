using System;
using Core.Enums;
using Core.Models;

namespace Core.Utils;

public static class MissionValidator
{
    public static bool IsValid(string title, string? description, DateTime dueDate,
        MissionKind kind, out string? error)
    {
        return IsCorrectTitle(title, out error)
               && IsCorrectDescription(description, out error) && IsNotInPast(dueDate, out error) &&
               IsUtc(dueDate, out error) && HasTimeIfMust(dueDate, kind, out error);
    }

    public static bool IsCorrectTitle(string title, out string? error)
    {
        error = null;
        if (string.IsNullOrWhiteSpace(title))
        {
            error = "Title cannot be null or whitespace.";
            return false;
        }

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
        if (description is null) return true;
        if (description.Length > Mission.DescriptionMaxLength)
            error =
                $"Description too long. Description must be less than {Mission.DescriptionMaxLength}.";
        return error is null;
    }

    private static bool IsNotInPast(DateTime dueDate, out string? error)
    {
        error = null;
        if (dueDate < DateTime.UtcNow)
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

    private static bool HasTimeIfMust(DateTime dueDate, MissionKind kind,
        out string? error)
    {
        error = null;
        if (kind is not MissionKind.OneTime)
            return true;
        if (dueDate.TimeOfDay == TimeSpan.Zero)
            error = "The due date doesn't have the time when it should.";
        return error is null;
    }
}