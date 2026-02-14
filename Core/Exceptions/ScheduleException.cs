using System;

namespace Core.Exceptions;

public abstract class ScheduleException(string? message) : Exception(message);