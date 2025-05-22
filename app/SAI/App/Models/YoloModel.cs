using System;

namespace SAI.App.Models
{
    public class InferenceResult
    {
        public bool Success { get; set; }
        public string ResultImage { get; set; }
        public double InferenceTime { get; set; }
        public string Error { get; set; }
    }
}