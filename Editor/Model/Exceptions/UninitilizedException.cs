using System;

namespace MenuGenerator.Editor.Model.Exceptions
{
    public class UninitializedException : Exception
    {
        public UninitializedException(string message) : base(message)
        {
        }
    }
}