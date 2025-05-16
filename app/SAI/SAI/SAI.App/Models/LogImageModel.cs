using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;

namespace SAI.SAI.App.Models
{
    public class LogImageModel
    {
        private static LogImageModel instance;
        private List<Bitmap> images;

        private LogImageModel()
        {
        }

        public static LogImageModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogImageModel();
                }
                return instance;
            }
        }

        public List<Bitmap> Images
        { 
            get { return images; }
            set { this.images = value; }
        }   
    }
}
