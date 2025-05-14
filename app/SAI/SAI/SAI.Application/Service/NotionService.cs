using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Service
{
    public class NotionService
    {
        private readonly HttpClient _http;
        private readonly string token;

        public NotionService(string token)
        {
            this.token = token;
        }
        public NotionService() { }



    }
}
