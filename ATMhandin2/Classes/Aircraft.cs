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

        public double CalculateVelocity(int xcor1, int xcor2, int ycor1, int ycor2, DateTime Timestamp1, DateTime Timestamp2)
        {
            double velocity = 0;

            double xdiff = xcor1 - xcor2;
            double ydiff = ycor1 - ycor2;
            double distance = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2));

            velocity = (int)(distance / (Timestamp1 - Timestamp2).TotalSeconds);

            return velocity;
        }

        public double CalculateAngle(double xcor1, double xcor2, double ycor1, double ycor2)
        {
            double xdiff = xcor1 - xcor2;
            double ydiff = ycor1 - ycor2;

            double angle = Math.Atan(xdiff / ydiff) * 180 / Math.PI;

            if (xdiff > 0 && ydiff > 0)
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
            HorizontalVelocity = (int)CalculateVelocity(td.XCoordinate, XCoordinate, td.YCoordinate, YCoordinate, td.TimeStamp, TimeStamp);

            //double xdiff = td.XCoordinate - XCoordinate;
            //double ydiff = td.YCoordinate - YCoordinate;
            //double distance = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2));

            //HorizontalVelocity = (int) (distance / (td.TimeStamp - TimeStamp).TotalSeconds);


            // Calculating angle
            //double angle = Math.Atan(xdiff / ydiff) * 180 / .thMath.PI;

            //if (xdiff > 0 && ydiff > 0)
            //{
            //    angle += 180;
            //}
            //else if (xdiff < 0 && ydiff > 0)
            //{
            //    angle += 360;
            //}

            CompassCourse = (int) CalculateAngle(td.XCoordinate, XCoordinate, td.YCoordinate, YCoordinate); 

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
