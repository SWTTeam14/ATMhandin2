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

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            Receiver = receiver;

            Receiver.TransponderDataReady += ReceiverTransponderDataReady;
        }
        
        private void ReceiverTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {

            var decoder = new Decoder();
            foreach (var data in e.TransponderData)
            {
                var td = decoder.convertData(data);

                var args = new TransponderDataItemEventArgs {TransponderDataItem = td};

                OnTransponderDataItemEvent(args);

                //Console.WriteLine(td.ToString());
            }
        }

        protected void OnTransponderDataItemEvent(TransponderDataItemEventArgs e)
        {
            TransponderDataItemEvent?.Invoke(this, e);
        }
    }
}
