﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Classes;

namespace ATMhandin2.Interfaces
{
    public interface IDecoder
    {
        TransponderDataItem convertData(string data);
    }
}
