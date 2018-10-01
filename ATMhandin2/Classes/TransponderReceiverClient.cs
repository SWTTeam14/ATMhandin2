using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Events;
using TransponderReceiver;

namespace ATMhandin2.Classes
{
    public delegate void TransponderDataItemHandler(object sender, TransponderDataItemEventArgs e);

    public class TransponderReceiverClient
    {
        public event TransponderDataItemHandler TransponderDataItemEvent;
        private ITransponderReceiver Receiver;

        Monitor mon = new Monitor();
        Aircraft a1 = new Aircraft("hej", 20000, 20000, 500);

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            Receiver = receiver;

            Receiver.TransponderDataReady += ReceiverTransponderDataReady;
        }


        private void ReceiverTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                Decoder decoder = new Decoder();
                TransponderDataItem td = decoder.convertData(data);

                TransponderDataItemEventArgs args = new TransponderDataItemEventArgs();
                args.TransponderDataItem = td;

                OnTransponderDataItemEvent(args);

                //Console.WriteLine(td.ToString());
            }
        }

        protected void OnTransponderDataItemEvent(TransponderDataItemEventArgs e)
        {
            TransponderDataItemEvent(this, e);
        }
    }
}
