using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Classes;
using ATMhandin2.Events;

namespace ATMhandin2.Interfaces
{
    public interface IAMSController
    {
        void TrcOnTransponderDataItemEvent(object sender, TransponderDataItemEventArgs e);
        void Print();
    }
}
