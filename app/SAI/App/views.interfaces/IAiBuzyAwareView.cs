using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.App.Views.Interfaces
{
    public enum BusyContext { Feedback, OAuth }
    public interface IAiBuzyAwareView
    {
        void SetBusy(BusyContext ctx, bool on);
    }
}
