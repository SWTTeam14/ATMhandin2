using System;
using System.Collections.Generic;
using ATMhandin2.Classes;
using ATMhandin2.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace Transponder.Receiver.Test
{

    public class TestTransponderReceiver
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private TransponderReceiverClient _uut;
        private IDecoder _fakeDecoder;

        [SetUp]
        public void setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);
            _fakeDecoder = Substitute.For<IDecoder>();
        }


        [Test]
        public void TestReception()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

        }

        [Test]
        public void TestDecoder()
        {
            string testData = "ATR423;39045;12932;14000;20151006213456789";
            
            TransponderDataItem td = _fakeDecoder.convertData(testData);

            Assert.That(_fakeDecoder.convertData(testData), Is.EqualTo(td));
        }
    }
}
