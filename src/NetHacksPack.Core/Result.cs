using System;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Core
{
    public struct Result<T>
    {
        public static implicit operator Result<T>(T data) => new Result<T>(data);
        public static implicit operator Result<T>(List<string> errors) => new Result<T>(errors);
        public static implicit operator Result<T>(string message) => new Result<T>(message);
        public static implicit operator T(Result<T> response) => response.Data;

        public Result(T data)
        {
            Data = data;
            Errors = null;
        }

        public Result(IEnumerable<string> errors)
        {
            Data = default(T);
            Errors = errors;
        }

        public Result(T data, IEnumerable<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public Result(string message)
        {
            Data = default(T);
            if (!string.IsNullOrWhiteSpace(message))
            {
                Errors = new List<string>(1) { message };
            }
            else
            {
                Errors = null;
            }
        }

        public T Data { get; }
        public IEnumerable<string> Errors { get; }

        public bool HasError() => Errors != null && Errors.Any();
    }
}
