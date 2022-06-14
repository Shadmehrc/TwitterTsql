using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.ApiResponse
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }


        public OkObjectResult Success(T data)
        {
            return new(new ApiResponse()
            {
                Error = null,
                IsSuccess = true,
                Data = data,
                Message = "Success",
            });
        }

        public BadRequestObjectResult FailedToFind(string Message)
        {
            return new(new ApiResponse()
            {
                Error = "Not Found",
                IsSuccess = false,
                Data = default,
                Message = Message,
            });
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
    }
}
