using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException() : base("This email already exists.")
        {
        }

        public DuplicateEmailException(string message) : base(message)
        {
        }
    }
}
