using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Hosting.Environment
{
    public class NotFoundEnvironmentException : Exception
    {
        public NotFoundEnvironmentException(string environmentVariable)
            : base($"Environment variable {environmentVariable} not found")
        {

        }
    }
}
