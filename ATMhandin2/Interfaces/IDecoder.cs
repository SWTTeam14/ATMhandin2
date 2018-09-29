using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMhandin2.Interfaces
{
    public interface IDecoder
    {
        string [] stringSplit(string data);
        string convertData(string[] data);

        DateTime convertTime(string [] data);

    }
}
