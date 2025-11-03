using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Exceptions
{
    public class DuplicateGameNameException : Exception
    {
        public DuplicateGameNameException() : base("A game with this name already exists.")
        {
        }

        public DuplicateGameNameException(string message) : base(message)
        {
        }
    }
}
