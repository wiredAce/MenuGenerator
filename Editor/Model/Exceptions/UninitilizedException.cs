using System;

namespace MenuGenerator.Editor.Model.Exceptions
{
    /// <summary>
    /// Exception that is thrown when an object is used before it's initialization
    /// has been called or independently completed
    /// </summary>
    public class UninitializedException : Exception
    {
        public UninitializedException(string message) : base(message)
        {
        }
    }
}