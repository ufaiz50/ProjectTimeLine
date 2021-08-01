using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectTimeLine.ViewModel
{
    public class JWTokenVM
    {
        public string Token { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
