using System;
using System.Threading;
using ATMhandin2.Classes;
using TransponderReceiver;

namespace ATMhandin2
{
    class Program
    {
        static void Main(string[] args)
        {
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            var t1 = new TransponderReceiverClient(receiver);

            AMSController amsController = new AMSController(t1);

            while (true)
            {
                Thread.Sleep(1000);
                Console.Clear();
                amsController.Print();
            }
        }
    }
}