using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    public class RestException:Exception
    {
        public RestException()
        {

        }

        public RestException(HttpStatusCode code, string key,string message)
        {
            ModelErrors = new List<KeyValuePair<string, string>>();
            ModelErrors.Add(new KeyValuePair<string,string>(key,message));
            Code = code;
        }
        public RestException(HttpStatusCode code, string message)
        {
            this.Message = message;
            this.Code = code;
        }
        public HttpStatusCode Code { get; set; }
        public List<KeyValuePair<string,string>> ModelErrors { get; set; } 
        public string Message { get; set; }


    }
}
