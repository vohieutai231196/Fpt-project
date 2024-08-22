using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Properties.Command
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; }
        public T? Data { get; }
        public string Message { get; }

        public ApiResponse(int statusCode, T? data, string message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message ?? string.Empty;
        }

        public static ApiResponse<T> Success(T? data, bool isSuccess, string message = "Operation was successful")
        {
            // if (data == null) throw new ArgumentNullException(nameof(data), "Data cannot be null for a successful response.");

            return new ApiResponse<T>(200, data, message);
        }
        public static ApiResponse<T> Failure(int statusCode, string message = "An error occurred")
        {
            return new ApiResponse<T>(statusCode, default!, message);
        }

        public static ApiResponse<T> NotFound(string message = "Resource not found")
        {
            return new ApiResponse<T>(404, default!, message);
        }

        public static ApiResponse<T> Error(string message = "An unexpected error occurred")
        {
            return new ApiResponse<T>(500, default!, message);
        }
    }
}

