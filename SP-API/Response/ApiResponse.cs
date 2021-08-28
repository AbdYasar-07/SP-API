using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP_API.Response
{
    public class ApiResponse
    {
        public string Message { get; set; }

        public bool IsInserted { get; set; }

        public int HttpStatus { get; set; }
    }
}
