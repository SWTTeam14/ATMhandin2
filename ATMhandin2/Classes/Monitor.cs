using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATMhandin2.Classes
{
    public class Monitor
    {
        public List<SpecifikAircraft> AircraftsInsideAirspace;

        public Monitor()
        {
            AircraftsInsideAirspace = new List<SpecifikAircraft>();
        }

        public void seperationEvent()
        {
            Console.WriteLine("Aircrafts inside airspace: " + AircraftsInsideAirspace.Count);

            foreach (var aircraft in AircraftsInsideAirspace)
            {
                for (int i = 0; i < AircraftsInsideAirspace.Count; i++)
                {
                    if (aircraft == AircraftsInsideAirspace[i])
                    {
                        i++;
                    } 

                    int diffAltitude = aircraft.Altitude - AircraftsInsideAirspace[i].Altitude;
                    double diffLongtitude = longTitude(AircraftsInsideAirspace[i].XCoordinate, aircraft.XCoordinate,
                        AircraftsInsideAirspace[i].YCoordinate, aircraft.YCoordinate);


                    if (diffAltitude < 300 && diffLongtitude < 5000)
                    {
                        Console.WriteLine(" WARNING!!!! devert course");
                        // Få fly til at ændre retning. 
                    }
                }
            }
        }

        public void FilterAirplanesOutsideAirspace(List<SpecifikAircraft> Aircrafts)
        {
            foreach (var data in Aircrafts)
            {
                if (CheckifInsideAirspace(data))
                {
                    AircraftsInsideAirspace.Add(data);
                }
                else if (!CheckifInsideAirspace(data) && AircraftsInsideAirspace.Count > 0)
                {
                    AircraftsInsideAirspace.Remove(data);
                }
            }
        }


        //Hjælpefunktion:
        public bool CheckifInsideAirspace(SpecifikAircraft aircraft)
        {
            if (aircraft.Altitude > 500 && aircraft.Altitude < 20000 && ((aircraft.XCoordinate < 90000 && aircraft.XCoordinate > 10000) && (aircraft.YCoordinate < 90000 && aircraft.YCoordinate > 10000)))
            {
                Console.WriteLine("The track is inside the monitored airspace.");
                Console.WriteLine("The track " + aircraft.Tag + " is inside the monitored airspace with the coordinates " + aircraft.XCoordinate + "," + aircraft.YCoordinate);
                return true;
            }

            return false;
        }
        
        // Hjælpefunktion
        public double longTitude(double x1coor, double x2coor, double y1coor, double y2coor)
        {
            double xdiif = x1coor - x2coor;
            double ydiif = y1coor - y2coor;
            double longtitude = Math.Sqrt(Math.Pow(xdiif, 2) - Math.Pow(ydiif, 2));

            return longtitude;
        }


    }
}
