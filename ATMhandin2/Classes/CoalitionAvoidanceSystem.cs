﻿using System;
using System.Collections.Generic;
using System.IO;
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
        private Aircraft _tmpAircraft;
        private Aircraft _tmpAircraftToCompare;

        public CoalitionAvoidanceSystem(AMSController amsController)
        {
            _amsController = amsController;
            
            _aircraftsInAirspace = _amsController._aircraftsInsideAirspace;
            
        }
        
        public bool CoalitionWarning()
        {

            for (int i = 0; i < _aircraftsInAirspace.Values.Count; i++)
            {
                for (int j = i+1; j < _aircraftsInAirspace.Values.Count; j++)
                {
                    int diffAltitude = _aircraftsInAirspace.ElementAt(i).Value.Altitude - _aircraftsInAirspace.ElementAt(j).Value.Altitude;

                    double diffLongtitude = distanceTo(
                        _aircraftsInAirspace.ElementAt(i).Value.XCoordinate,
                        _aircraftsInAirspace.ElementAt(j).Value.XCoordinate,
                        _aircraftsInAirspace.ElementAt(i).Value.YCoordinate,
                        _aircraftsInAirspace.ElementAt(j).Value.YCoordinate);

                    if (diffAltitude < 300 && diffLongtitude < 5000)
                    {
                        _tmpAircraft = _aircraftsInAirspace.ElementAt(i).Value;
                        _tmpAircraftToCompare = _aircraftsInAirspace.ElementAt(j).Value;

                        FileStream fs = new FileStream(@"C:\Users\Public\TestFolder\WriteLines.txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        TextWriter tw = Console.Out;
                        
                        Console.SetOut(sw);
                        Console.WriteLine("WARNING!!!! {0}, you are on a coalition course with {1}. At: {2}. Divert course!", _aircraftsInAirspace.ElementAt(i).Value.Tag, _aircraftsInAirspace.ElementAt(j).Value.Tag, _aircraftsInAirspace.ElementAt(i).Value.TimeStamp);
                        Console.SetOut(tw);

                        sw.Close();
                        fs.Close();
                        
                        return true;
                    }
                }
            }
            Console.WriteLine("No current coalition warnings");
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
