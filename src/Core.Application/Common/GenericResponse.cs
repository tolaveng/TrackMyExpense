using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common
{
    public class GenericResponse<T>// where T : class
    {
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }

        public GenericResponse(bool succeeded, T? data, string message, string[] errors)
        {
            Succeeded = succeeded;
            Data = data;
            Message = message;
            Errors = errors;
        }

        public static GenericResponse<T> Success()
        {
            return new GenericResponse<T>(true, default, "", Array.Empty<string>());
        }

        public static GenericResponse<T> Success(string message)
        {
            return new GenericResponse<T>(true, default, message, Array.Empty<string>());
        }

        public static GenericResponse<T> Success(T data, string message)
        {
            return new GenericResponse<T>(true, data, message, Array.Empty<string>());
        }

        public static GenericResponse<T> Success(T data)
        {
            return new GenericResponse<T>(true, data, string.Empty, Array.Empty<string>());
        }

        public static GenericResponse<T> Failure(string message, string[]? errors = null)
        {
            if (errors == null) return new GenericResponse<T>(true, default, message, Array.Empty<string>());
            return new GenericResponse<T>(true, default, message, errors);
        }
    }
}
