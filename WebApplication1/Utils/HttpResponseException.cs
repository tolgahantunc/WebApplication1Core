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
        public string MessageToClient { get; set; }

        public Exception InnerExceptionToLog { get; set; }

        public HttpResponseException(string message, Exception realException)
        {
            MessageToClient = message;
            InnerExceptionToLog = realException;
        }
    }
}
