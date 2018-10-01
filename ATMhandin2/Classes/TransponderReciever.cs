using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATMhandin2.Classes
{
    
    public class TransponderReceiver
    {
        private ITransponderReceiver Receiver;
        

        Decoder decode = new Decoder();
        Monitor mon = new Monitor();
        SpecifikAircraft a1 = new SpecifikAircraft("hej", 20000, 20000, 500);

        public TransponderReceiver(ITransponderReceiver receiver)
        {
            Receiver = receiver;

            Receiver.TransponderDataReady += RecieverTransponderDataReady;
        }


        private void RecieverTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            SpecifikAircraft a2 = new SpecifikAircraft("MUH120", 15000,15000,1500);
            SpecifikAircraft a3 = new SpecifikAircraft("OHH10", 15400,15400,1500);

            decode.Aircrafts.Add(a2);
            decode.Aircrafts.Add(a3);

            foreach (var data in e.TransponderData)
            {

                decode.ConvertDataToAircraft(data);

                SpecifikAircraft a1 = decode.ConvertDataToAircraft(data);

                mon.CheckifInsideAirspace(a1);
                mon.seperationEvent();




            }
            
        }
    }
}
