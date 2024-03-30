using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Domain.Common
{
    public class Result
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public static Result Succeed(int statusCode = 200)
        {
            return new Result
            {
                Success = true,
                StatusCode = statusCode,
            };
        }

        public static Result Failed(string message, int statusCode = 500)
        {
            return new Result
            { 
                Success = false, 
                Message = message,
                StatusCode = statusCode 
            };
        }
    }

    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public T Value { get; set; }

        public static Result<T> Succeed(T value, int statusCode = 200)
        {
            return new Result<T>
            {
                Success = true,
                StatusCode = statusCode,
                Value = value,
            };
        }

        public static Result<T> Fail(string message, int statusCode = 500)
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
            };
        }
    }
}
