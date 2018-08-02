using System;
using System.Net;

namespace DataValidations.PhoneNumber.Models
{
    public class GenericResponse<T>
    {

        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

    }
}
