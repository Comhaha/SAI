using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Dto
{
    public class AiFeedbackRequestDto
    {
        public string code { get; set; } = string.Empty;
        public string log { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
    }
}
