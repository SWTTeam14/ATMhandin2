using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMhandin2.Classes
{
    public class Monitor
    {
        //Dette er lige en test
        public bool isInsideAirspace(SpecifikAircraft aircraft)
        {
            if (aircraft.Altitude > 500 && aircraft.Altitude < 20000 && ((aircraft.XCoordinate > 10000 && aircraft.YCoordinate > 10000) || (aircraft.XCoordinate < 90000 && aircraft.YCoordinate < 90000)))
            {
                Console.WriteLine("The track is inside the monitored airspace.");
                return true;
            }

            return false;
        }


        public bool IsInsideAirSpace(List<SpecifikAircraft> Aircrafts)
        {
            foreach (var data in Aircrafts)
            {
                if (isInsideAirspace(data) == true)
                {

                }
               
            }
            return true;
        }

        //public bool seperation()
        //{

        //}
    }
}
