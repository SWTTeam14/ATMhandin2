using System;
using System.Collections.Generic;
using ATMhandin2.Interfaces;
using ATMhandin2.Classes;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace Transponder.Receiver.Test
{

    public class TestTransponderReceiver
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private TransponderReceiverClient _uut;
        private Decoder D1;
        private IDecoder _decoder;

        [SetUp]
        public void setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _decoder = Substitute.For<IDecoder>();
            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);
            
            D1 = new Decoder();
        }

        [Test]
        public void TestReception()
        {
            //SETUP
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            //ACT
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            //ASSERT
            

        }
    }
}
