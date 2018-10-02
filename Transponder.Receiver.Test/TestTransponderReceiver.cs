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
        private IAMSController _fakeAmsController;
        private TransponderDataItem _fakeTdItem;


        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);

            _fakeDecoder = Substitute.For<IDecoder>();

            _fakeAmsController = Substitute.For<IAMSController>();

            _fakeTdItem = new TransponderDataItem();

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
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
        
            //ASSERT
            
        }

        [Test]
        public void TestDecoder()
        {
            string testData = "ATR423;39045;12932;14000;20151006213456789";
            
            TransponderDataItem td = _fakeDecoder.convertData(testData);

            Assert.That(_fakeDecoder.convertData(testData), Is.EqualTo(td));

            
        }

        [Test]
        public void TestTostring()
        {
            string TestString =
                "Tag:\t\tATR423\nX coordinate:\t39045 meters\nY coordinate:\t12932 meters\nAltitude:\t14000 meters\nTimestamp:\tJune 10, 2015 21:34:56 789\n";
            string testData = "ATR423;39045;12932;14000;20151006213456789";

            _fakeTdItem = _fakeDecoder.convertData(testData);

            Assert.That(TestString, Is.EqualTo(_fakeTdItem.ToString()));
            //Assert.AreSame(TestString, _fakeTdItem.ToString());

        }

        [Test]
        public void TestAMSController()
        {
            
        }
    }
}
