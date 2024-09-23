using System;

public class UninitializedException : Exception
{
    public UninitializedException(string message) : base(message)
    {
    }
}