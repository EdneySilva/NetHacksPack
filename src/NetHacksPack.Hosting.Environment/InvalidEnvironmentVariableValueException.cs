using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Hosting.Environment
{
    public class InvalidEnvironmentVariableValueException : Exception
    {
        public InvalidEnvironmentVariableValueException(string environmentVariable, object value)
            : base($"Environment variable {environmentVariable} has invalid value: {value}")
        {

        }
    }
}
