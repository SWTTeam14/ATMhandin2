using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Classes;

namespace ATMhandin2.Interfaces
{
    public interface IDecoder
    {
        Classes.SpecifikAircraft ConvertDataToAircraft(string data);
        string convertData(string[] data);
        DateTime convertTime(string [] data);

    }
}
