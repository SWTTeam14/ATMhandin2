using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Classes;

namespace ATMhandin2.Events
{
    public class TransponderDataItemEventArgs : EventArgs
    {
        public TransponderDataItem TransponderDataItem { get; set; }
    }
}
