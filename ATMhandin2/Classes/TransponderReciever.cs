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
 

            foreach (var data in e.TransponderData)
            {
                SpecifikAircraft a1 = decode.ConvertDataToAircraft(data);
                
                mon.FilterAirplanesOutsideAirspace(decode.Aircrafts);

                mon.seperationEvent();

                for (int i = 0; i < decode.Aircrafts.Count; i++)
                {
                    Console.WriteLine("Fly: " + decode.Aircrafts[i].Tag);
                }
            }




        }
    }
}
