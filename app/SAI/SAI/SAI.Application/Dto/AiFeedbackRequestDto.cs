using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Dto
{
    public class AiFeedbackRequestDto
    {
        [Required] public string code { get; set; } = string.Empty;   // 코드 원문
        [Required] public string logImage { get; set; } = string.Empty;   // 학습/실행 로그 이미지 (상대 경로)
        [Required] public string resultImage { get; set; } = string.Empty;   // 결과 이미지 (상대 경로)
        [Required] public string memo { get; set; } = string.Empty;

        [Required] 
        [Range(0.01, 1.00)]
        public double threshold { get; set; }
    }
}
