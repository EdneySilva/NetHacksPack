using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Hosting.Environment
{
    public static class EnvironmentVariableHelper
    {
        static Dictionary<string, Func<string, object>> parserProviders = new Dictionary<string, Func<string, object>>();
        static readonly string[] trueBooleanParsers = new[] { "YES", "Y", "SIM", "S", "TRUE", "T" };
        static readonly string[] falseBooleanParsers = new[] { "NO", "N", "NAO", "FALSE", "F" };

        public static T Get<T>(this string variableName, T defaultValue = default(T))
        {
            if (!parserProviders.ContainsKey(typeof(T).Name))
                return defaultValue;
            var environmentValue = System.Environment.GetEnvironmentVariable(variableName);
            if (string.IsNullOrEmpty(environmentValue?.Trim()))
                return defaultValue;
            return (T)parserProviders[typeof(T).Name](environmentValue);
        }

        public static bool GetBool(this string variableName, bool defaultValue = false, bool throwExceptionIfNotFoundOrInvalid = false)
        {
            var value = variableName.GetString(variableName, throwExceptionIfNotFoundOrInvalid: throwExceptionIfNotFoundOrInvalid);

            if (trueBooleanParsers.Contains(value))
                return true;
            if (falseBooleanParsers.Contains(value))
                return false;
            if (throwExceptionIfNotFoundOrInvalid)
                throw new InvalidEnvironmentVariableValueException(variableName, value);
            return defaultValue;
        }

        public static int GetInt(this string variableName, int defaultValue = 0, bool throwExceptionIfNotFoundOrInvalid = false)
        {
            var value = variableName.GetString(variableName, throwExceptionIfNotFoundOrInvalid: throwExceptionIfNotFoundOrInvalid);
            if (int.TryParse(value, out int result))
                return result;
            if (throwExceptionIfNotFoundOrInvalid)
                throw new InvalidEnvironmentVariableValueException(variableName, value);
            return defaultValue;
        }

        public static long GetLong(this string variableName, long defaultValue = 0, bool throwExceptionIfNotFoundOrInvalid = false)
        {
            var value = variableName.GetString(variableName, throwExceptionIfNotFoundOrInvalid: throwExceptionIfNotFoundOrInvalid);
            if (long.TryParse(value, out long result))
                return result;
            if (throwExceptionIfNotFoundOrInvalid)
                throw new InvalidEnvironmentVariableValueException(variableName, value);
            return defaultValue;
        }

        public static string GetString(this string variableName, string defaultValue = default(string), bool throwExceptionIfNotFoundOrInvalid = false)
        {
            var environmentValue = System.Environment.GetEnvironmentVariable(variableName);
            if (throwExceptionIfNotFoundOrInvalid && string.IsNullOrEmpty(environmentValue))
                throw new NotFoundEnvironmentException(variableName);
            if (string.IsNullOrEmpty(environmentValue?.Trim()))
                return defaultValue;
            if (throwExceptionIfNotFoundOrInvalid)
                throw new InvalidEnvironmentVariableValueException(variableName, environmentValue);
            return environmentValue;
        }

        public static bool TryGetJson<T>(this string variableName, out T value, bool throwExcpetionIfInvalid = false)
        {
            value = default(T);
            try
            {
                var json = variableName.GetString(string.Empty);
                if (string.IsNullOrEmpty(json))
                    return false;
                value = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (Exception ex)
            {
                if (throwExcpetionIfInvalid)
                    throw ex;
                return false;
            }
        }
    }
}
