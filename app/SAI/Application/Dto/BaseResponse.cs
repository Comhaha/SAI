using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Dto
{
    public class BaseResponse<T>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public T result { get; set; }
    }
}
