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

        public double CalculateVelocity(int xcoor1, int xcoor2, int ycoor1, int ycoor2, DateTime timestamp1, DateTime timestamp2)
        {
            double xdiff = xcoor2 - xcoor1;
            double ydiff = ycoor2 - ycoor1;

            double distance = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2));

            double velocity = (int)(distance / (timestamp2 - timestamp1).TotalSeconds);

            return velocity;
        }

        public double CalculateAngle(double xcoor1, double xcoor2, double ycoor1, double ycoor2)
        {
            double xdiff = xcoor2 - xcoor1;
            double ydiff = ycoor2 - ycoor1;

            double angle = Math.Atan(xdiff / ydiff) * 180 / Math.PI;

            if (ydiff < 0)
            {
                angle += 180;
            }
            else if (xdiff < 0 && ydiff > 0)
            {
                angle += 360;
            }
           return angle;
        }

        public void Update(ITransponderDataItem td)
        {
            //Calculating velocity
            HorizontalVelocity = (int)CalculateVelocity(XCoordinate, td.XCoordinate, YCoordinate, td.YCoordinate, TimeStamp, td.TimeStamp);

            CompassCourse = (int) CalculateAngle(XCoordinate, td.XCoordinate, YCoordinate, td.YCoordinate); 

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
