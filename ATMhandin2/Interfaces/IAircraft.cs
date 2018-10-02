using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMhandin2.Interfaces
{
    public interface IAircraft
    {
        string Tag { get; set; }
        double XCoordinate { get; set; }
        double YCoordinate { get; set; }
        int Altitude { get; set; }
        DateTime TimeStamp { get; set; }
        double CompassCourse { get; set; }
        double HorizontalVelocity { get; set; }
        void Update(ITransponderDataItem td);

        string ToString();

    }
}
