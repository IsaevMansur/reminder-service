using System;
using Core.Enums;
using Core.Exceptions;

namespace Core.Models;

public class Mission
{
    public const int TitleMaxLength = 32;
    public const int TitleMinLength = 1;
    public const int DescriptionMaxLength = 256;

    private Mission(Guid id, string title, string? description, DateTime dueDate,
        MissionKind kind)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Kind = kind;
    }

    public Guid Id { get; private set; }

    public DateTime DueDate { get; private set; }

    public MissionKind Kind { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public bool IsCompleted { get; private set; }

    public static Mission Create(Guid id, string title, DateTime dueDate,
        MissionKind kind = MissionKind.Unscheduled,
        string? description = null)
    {
        CreateMissionException.ThrowIfInvalid(title, dueDate, kind, description);
        return new Mission(id, title, description, dueDate, kind);
    }

    public void Rename(string title)
    {
        ChangeMissionException.ThrowIfInvalidTitle(title);
        Title = title;
    }

    public void Describe(string? description = null)
    {
        ChangeMissionException.ThrowIfInvalidDescription(description);
        Description = description;
    }

    public void Complete()
    {
        if (IsCompleted)
            throw new ChangeMissionException("Mission already completed");
        IsCompleted = true;
    }

    public void Reopen()
    {
        if (!IsCompleted)
            throw new ChangeMissionException("Mission not completed");
        IsCompleted = false;
    }
}