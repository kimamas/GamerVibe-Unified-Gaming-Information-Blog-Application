using System;

namespace BLLClassLibrary.Exceptions
{
    public class DuplicateBlogNameException : Exception
    {
        public DuplicateBlogNameException() : base("A blog with the name already exists.")
        {
        }
        public DuplicateBlogNameException(string message)
            : base(message)
        {
        }
    }
}
