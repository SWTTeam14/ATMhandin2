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

        

        public TransponderReceiver(ITransponderReceiver receiver)
        {
            Receiver = receiver;

            Receiver.TransponderDataReady += RecieverTransponderDataReady;
        }


        private void RecieverTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                System.Console.WriteLine($"Transponderdata {data}");

                decode.ConvertDataToAircraft(data);


            }
        }
    }
}
