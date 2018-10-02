using System;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    public class Aircraft : IAircraft
    {
        public Aircraft(string tag, int xCoordinate, int yCoordinate, int altitude, double compassCourse)
        {
            Tag = tag;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Altitude = altitude;
            this.TimeStamp = DateTime.Now;
            CompassCourse = compassCourse;

        }

        public string Tag { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public double CompassCourse { get; set; }

        public double HorizontalVelocity { get; set; }

        public void Update(ITransponderDataItem td)
        {
            XCoordinate = td.XCoordinate;
            YCoordinate = td.YCoordinate;
            Altitude = td.Altitude;
            TimeStamp = td.TimeStamp;
        }

        public override string ToString()
        {
            string dateTimeString = TimeStamp.ToString("MMMM dd, yyyy HH:mm:ss fff");
            return string.Format("Tag:\t\t{0}\nX coordinate:\t{1} meters\nY coordinate:\t{2} meters\nAltitude:\t{3} meters\nTimestamp:\t{4}\nCompassCourse:\t{5}\n", Tag, XCoordinate, YCoordinate, Altitude, dateTimeString, CompassCourse);
        }

        public void Calculation()
        {
            DateTime now = DateTime.Now;
            double totalMilliseconds = (now - this.TimeStamp).TotalMilliseconds;
            double num1 = -this.CompassCourse + 90.0;
            if (num1 <= -180.0)
                num1 += 360.0;
            double num2 = num1 * Math.PI / 180.0;
            XCoordinate += Math.Cos(num2) * (this.HorizontalVelocity * totalMilliseconds / 1000.0);
            YCoordinate += Math.Sin(num2) * (this.HorizontalVelocity * totalMilliseconds / 1000.0);
            TimeStamp = now;
        }

    }
}
