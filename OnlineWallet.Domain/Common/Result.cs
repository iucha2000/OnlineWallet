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
                StatusCode = statusCode,
                Success = true
            };
        }

        public static Result Failed(string message, int statusCode = 500)
        {
            return new Result
            { 
                StatusCode = statusCode,
                Success = false,
                Message = message
            };
        }
    }

    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public T? Value { get; set; }

        public static Result<T> Succeed(T value, int statusCode = 200)
        {
            return new Result<T>
            {
                StatusCode = statusCode,
                Success = true,
                Value = value,
            };
        }

        public static Result<T> Failed(string message, int statusCode = 500)
        {
            return new Result<T>
            {
                StatusCode = statusCode,
                Success = false,
                Message = message
            };
        }
    }
}
