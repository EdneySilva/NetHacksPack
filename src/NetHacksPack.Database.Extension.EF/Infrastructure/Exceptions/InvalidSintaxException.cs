using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extension.EF.Infrastructure.Exceptions
{
    public class InvalidSintaxException : Exception
    {
        public InvalidSintaxException(string message)
            : base("Invalid sintax: " + message)
        {

        }
    }
}
