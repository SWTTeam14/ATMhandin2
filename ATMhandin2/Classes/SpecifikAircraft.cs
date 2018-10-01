using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMhandin2.Classes
{
    public class SpecifikAircraft
    {
        public SpecifikAircraft(string tag, int xCoordinate, int yCoordinate, int altitude)
        {
            Tag = tag;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Altitude = altitude;
           
        }

        public SpecifikAircraft()
        {
            ;}
        
        void printAircraft()
        {
            Console.WriteLine();
        }
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
