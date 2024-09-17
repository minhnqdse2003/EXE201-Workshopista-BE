using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, T data, string message = null, List<string> errors = null)
        {
            Success = success;
            Data = data;
            Message = message;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Request was successful.")
        {
            return new ApiResponse<T>(true, data, message);
        }

        public static ApiResponse<T> ErrorResponse(string message, List<string> errors = null)
        {
            return new ApiResponse<T>(false, default, message, errors);
        }
    }

}
