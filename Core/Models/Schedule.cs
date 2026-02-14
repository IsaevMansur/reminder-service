using System;
using Core.Enums;
using Core.Exceptions;

namespace Core.Models;

public class Schedule
{
    private Schedule(DateTime dueDate, ScheduleKind kind)
    {
        DueDate = dueDate;
        Kind = kind;
    }

    public DateTime DueDate { get; private set; }

    public ScheduleKind Kind { get; private set; }

    public static Schedule Create(DateTime dueDate, ScheduleKind kind = ScheduleKind.None)
    {
        CreateScheduleException.ThrowIfInvalid(dueDate, kind);
        return new Schedule(dueDate, kind);
    }

    public void Reschedule(DateTime dueDate)
    {
        ChangeScheduleException.ThrowIfCannotReschedule(dueDate, Kind);
        DueDate = dueDate;
    }

    public void Reclassify(ScheduleKind kind)
    {
        Kind = kind;
    }
}