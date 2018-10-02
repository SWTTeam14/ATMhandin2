using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    public class TransponderDataItem : ITransponderDataItem
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            string dateTimeString = TimeStamp.ToString("MMMM dd, yyyy HH:mm:ss fff");
            return string.Format("Tag:\t\t{0}\nX coordinate:\t{1} meters\nY coordinate:\t{2} meters\nAltitude:\t{3} meters\nTimestamp:\t{4}\n", Tag, XCoordinate, YCoordinate, Altitude, dateTimeString);
        }
    }
}
