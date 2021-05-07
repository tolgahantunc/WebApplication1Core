using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public object Value { get; set; }
        public string Message { get; set; }

        public HttpResponseException(string message)
        {
            Message = message;
        }
    }
}
