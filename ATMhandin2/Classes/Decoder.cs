using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Interfaces;
using TransponderReceiver;

namespace ATMhandin2.Classes
{
    public class Decoder : IDecoder
    {
        public Decoder( )
        {
            
        }


       public  string[] stringSplit(string data)
        {
            string[] tokens;
            char[] seperators = { ';' };
            tokens = data.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

            return tokens;
        }

        public string convertData(string[] data)
        {

            string tag = data[1];
            string tab = "\t";

            DateTime dateTime1 = convertTime(data);

            string tis = "tag:          " + tab + data[0] + "\r\n" + "X coordinate:  " + tab + data[1] + "meters        \r\n" +
                         "Y Coordinate" + tab + data[2]
                         + "meters \r\n" + "Altitude: " + tab + data[3] + "meters \r\n" + "Timestamp: " + tab +
                         dateTime1;
            return tis;

        }

        public DateTime convertTime(string []data)
        {
            int year = int.Parse(data[4].Substring(0, 4));
            int month = int.Parse(data[4].Substring(4, 2));
            int day = int.Parse(data[4].Substring(6, 2));
            int hour = int.Parse(data[4].Substring(8, 2));
            int minute = int.Parse(data[4].Substring(10, 2));
            int second = int.Parse(data[4].Substring(12, 2));
            int ms = int.Parse(data[4].Substring(14, 3));

            DateTime dateTime1 = new DateTime(year, month, day, hour, minute, second, ms);
            return dateTime1;
        }
    }
}
