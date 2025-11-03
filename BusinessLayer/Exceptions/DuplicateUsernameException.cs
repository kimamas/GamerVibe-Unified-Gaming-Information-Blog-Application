using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Exceptions
{
    public class DuplicateUsernameException : Exception
    {
        public DuplicateUsernameException() : base("This username already exists.")
        {
        }
        public DuplicateUsernameException(string message) : base(message)
        {
        }
    }
}
