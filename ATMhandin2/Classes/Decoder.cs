using System;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    public class Decoder : IDecoder
    {
        public TransponderDataItem convertData(string data)
        {
            string[] tokens;
            char[] separators = { ';' };
            tokens = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            TransponderDataItem td = new TransponderDataItem();

            td.Tag = tokens[0];
            td.XCoordinate = int.Parse(tokens[1]);
            td.YCoordinate = int.Parse(tokens[2]);
            td.Altitude = int.Parse(tokens[3]);
            td.TimeStamp = convertTime(tokens[4]);

            return td;
        }

        //Hjælpe funktion
        private DateTime convertTime(string data)
        {

            DateTime myDate = DateTime.ParseExact(data, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);

            return myDate;
        }
    }
}
