using System;

namespace Core.Exceptions;

public abstract class MissionException(string? message) : Exception(message);