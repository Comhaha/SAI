using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
    public class NotionModel
    {
        private static NotionModel instance;

        private string redirectUrl;

        private NotionModel()
        {
            this.redirectUrl = "";
        }
        
        public static NotionModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotionModel();
                }
                return instance;
            }
        }

        public string RedirectUrl
        { get { return this.redirectUrl; } set { this.redirectUrl = value; }}
    }
}
