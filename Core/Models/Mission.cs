using System;
using Core.Exceptions;

namespace Core.Models;

public class Mission
{
    public const int TitleMaxLength = 32;
    public const int TitleMinLength = 1;
    public const int DescriptionMaxLength = 256;

    private Mission(Guid id, string title, string? description, Schedule schedule)
    {
        Id = id;
        Title = title;
        Description = description;
        Schedule = schedule;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public Schedule Schedule { get; private set; }

    public bool IsCompleted { get; private set; } = false;

    public static Mission Create(Guid id, string title,
        Schedule schedule, string? description = null)
    {
        CreateMissionException.ThrowIfInvalid(title, description);
        return new Mission(id, title, description, schedule);
    }

    public void Rename(string title)
    {
        ChangeMissionException.ThrowIfInvalidTitle(title);
        Title = title;
    }

    public void Describe(string description)
    {
        ChangeMissionException.ThrowIfInvalidDescription(description);
        Description = description;
    }
}