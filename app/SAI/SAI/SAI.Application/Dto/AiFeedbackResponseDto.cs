using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Dto
{
    public sealed class AiFeedbackResponseDto
    {
        [Required] public string feedbackId { get; set; }
        [Required] public string redirectUrl { get; set; }
        [Required] public string feedback { get; set; }
    }
}
