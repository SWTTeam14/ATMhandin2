using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    //Referred to as "Seperation Event" in assignment
    public class CoalitionAvoidanceSystem
    {
        private AMSController _amsController;
        public Dictionary<string, Aircraft> _aircraftsInAirspace;

        public CoalitionAvoidanceSystem(AMSController amsController)
        {
            _amsController = amsController;

            _aircraftsInAirspace = new Dictionary<string, Aircraft>();
            _aircraftsInAirspace = _amsController._aircraftsInsideAirspace;
        }

        public void seperate()
        {
            if (CoalitionWarning() == true)
            {
                
            }
            else
            {
                return;
            }
        }

        public bool CoalitionWarning()
        {
            foreach (Aircraft aircraft in _aircraftsInAirspace.Values)
            {
                
                for (int i = 0; i < _aircraftsInAirspace.Count; i++)
                {
                    int diffAltitude = aircraft.Altitude - _aircraftsInAirspace.ElementAt(i - 1).Value.Altitude;
                    double diffLongtitude = distanceTo(_aircraftsInAirspace.ElementAt(i).Value.XCoordinate, aircraft.XCoordinate,
                        _aircraftsInAirspace.ElementAt(i).Value.YCoordinate, aircraft.YCoordinate);

                    if (diffAltitude < 300 && diffLongtitude < 5000)
                    {
                        Console.WriteLine("The two planes are on a coalition course");
                        return true;
                    }

                    Console.WriteLine("The two planes are not on a coalition course");
                    return false;

                }
            }

            Console.WriteLine("Test line");
            return false;
        }

        public double distanceTo(double x1coor, double x2coor, double y1coor, double y2coor)
        {
            double xdiif = x1coor - x2coor;
            double ydiif = y1coor - y2coor;
            double longtitude = Math.Sqrt(Math.Pow(xdiif, 2) + Math.Pow(ydiif, 2));

            return longtitude;
        }
    }
}
