using System;

namespace ATMhandin2.Interfaces
{
    public interface ITransponderDataItem
    {
        int XCoordinate { get; set; }
        int YCoordinate { get; set; }
        int Altitude { get; set; }
        DateTime TimeStamp { get; set; }
        string ToString();
    }
}
