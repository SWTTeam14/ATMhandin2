using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Classes;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;


namespace Transponder.Receiver.Test
{
    
    public class TestTransponderReceiver
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private ATMhandin2.Classes.TransponderReceiver _uut;

        [SetUp]
        public void setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
           
        }
    }
}
