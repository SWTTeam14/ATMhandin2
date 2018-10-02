using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Interfaces;
using TransponderReceiver;

namespace ATMhandin2.Classes
{
    
    public class TransponderReceiver
    {
        private ITransponderReceiver Receiver;
        private IDecoder _decoder;

        Decoder decode = new Decoder();
        Monitor mon = new Monitor();
        
        public TransponderReceiver(ITransponderReceiver receiver)
        {
            Receiver = receiver;

            Receiver.TransponderDataReady += RecieverTransponderDataReady;
        }


        private void RecieverTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            
            foreach (var data in e.TransponderData)
            {

                decode.ConvertDataToAircraft(data);

                SpecifikAircraft a1 = decode.ConvertDataToAircraft(data);
                
                mon.seperationEvent();
                
            }
            
        }
    }
}
