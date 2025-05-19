using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
    public interface ICsvChartView
    {
        void SetData();
        void ShowErrorMessage(string message);
    }
}
