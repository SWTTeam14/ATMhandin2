using System;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    public class Aircraft : IAircraft
    {
        public Aircraft(string tag, int xCoordinate, int yCoordinate, int altitude)
        {
            Tag = tag;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Altitude = altitude;
        }

        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CompassCourse { get; set; }
        public int HorizontalVelocity { get; set; }

        public void Update(ITransponderDataItem td)
        {
            double xdiff = td.XCoordinate - XCoordinate;
            double ydiff = td.YCoordinate - YCoordinate;
            double distance = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2));

            HorizontalVelocity = (int) (distance / (td.TimeStamp - TimeStamp).TotalSeconds);

            double angle = Math.Atan(xdiff / ydiff) * 180 / Math.PI;

            if (xdiff > 0 && ydiff > 0)
            {
                angle += 180;
            }
            else if (xdiff < 0 && ydiff > 0)
            {
                angle += 360;
            }

            CompassCourse = (int) angle; 

            XCoordinate = td.XCoordinate;
            YCoordinate = td.YCoordinate;
            Altitude = td.Altitude;
            TimeStamp = td.TimeStamp;
        }

        public override string ToString()
        {
            string dateTimeString = TimeStamp.ToString("MMMM dd, yyyy HH:mm:ss fff");
            return string.Format("Tag:\t\t\t{0}\nX coordinate:\t\t{1} meters\nY coordinate:\t\t{2} meters\nAltitude:\t\t{3} meters\nTimestamp:\t\t{4}\nCompassCourse:\t\t{5}\nHorizontalVelocity:\t{6}\n", Tag, XCoordinate, YCoordinate, Altitude, dateTimeString, CompassCourse, HorizontalVelocity);
        }


    }
}
