using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Events;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    public class AMSController : IAMSController
    {
        

        public AMSController(TransponderReceiverClient trc)
        {
            _airspace = new Airspace(10000, 10000, 90000, 90000, 500, 20000);
            _trc = trc;

            //Subscribe to transponderreceiverclient 
            _trc.TransponderDataItemEvent += TrcOnTransponderDataItemEvent;
            

        }

        public void TrcOnTransponderDataItemEvent(object sender, TransponderDataItemEventArgs e)
        {
            TransponderDataItem td = e.TransponderDataItem;
            //Console.WriteLine(e.TransponderDataItem.ToString());
            if (_airspace.IsAircraftInside(e.TransponderDataItem))
            {
                if (_aircraftsInsideAirspace.ContainsKey(td.Tag))
                {
                    //Update aircraft data
                    _aircraftsInsideAirspace[td.Tag].Update(td);
                }
                else
                {
                    //Add aircraft if it doesn't exists. 
                    Aircraft aircraft = new Aircraft(td.Tag, td.XCoordinate, td.YCoordinate, td.Altitude);
                    _aircraftsInsideAirspace.Add(td.Tag, aircraft);
                }    
            }
            else
            {
                if (_aircraftsInsideAirspace.ContainsKey(td.Tag))
                {
                    //Remove aircraft because it is outside
                    _aircraftsInsideAirspace.Remove(td.Tag);
                }
            }
        }

        public void Print()
        {
            foreach (KeyValuePair<string, Aircraft> entry in _aircraftsInsideAirspace)
            {
                Console.WriteLine(entry.Value.ToString());;

            }
        }
        
        //This attribute could be changed to a list, så that events can be received from multiple responderreceiverclient
        private TransponderReceiverClient _trc;
        public Dictionary<string, Aircraft> _aircraftsInsideAirspace = new Dictionary<string, Aircraft>(); 
        private Airspace _airspace;
    }
}
