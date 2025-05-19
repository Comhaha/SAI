using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Dto
{
    public class AiFeedbackRequestDto
    {
        public string code { get; set; } = string.Empty;   // 코드 원문
        public string logImage { get; set; } = string.Empty;   // 학습/실행 로그 이미지 (상대 경로)
        public string resultImage { get; set; } = string.Empty;   // 결과 이미지 (상대 경로)
        public string memo { get; set; } = string.Empty;
        public double threshold { get; set; }
    }
}
