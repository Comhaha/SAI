using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Application.Dto
{
    public sealed class AiFeedbackResponseDto
    {
        public string feedbackId { get; set; }
        public string redirectUrl { get; set; }
        public string feedback { get; set; }
    }
}
