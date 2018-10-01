using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATMhandin2.Classes;
using TransponderReceiver;
using Decoder = ATMhandin2.Classes.Decoder;

namespace ATMhandin2
{
    class ATM
    {
        static void Main(string[] args)
        {
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            var t1 = new Classes.TransponderReceiver(receiver);




            //string d = "ATR423;39045;12932;14000;20151006213456789";
            //Decoder de = new Decoder();

            //string[] hej;
            //hej = de.stringSplit(d);
            //d = de.convertData(hej);

            //Console.WriteLine(d);


            while (true)
            {
               
                
            }
        }
    }
}
