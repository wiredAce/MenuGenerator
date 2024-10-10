using System;

namespace MenuGenerator.Editor.Model.Exceptions
{
    /// <summary>
    /// An exception that is thrown when input validation fails
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
