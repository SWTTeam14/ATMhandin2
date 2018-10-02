using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        private Airspace _fakeAirspace;
        // private ITransponderDataItem _fakeTdItem;


        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);

            _fakeDecoder = Substitute.For<IDecoder>();

            _fakeAmsController = Substitute.For<IAMSController>();

            _fakeAirspace = new Airspace(10000, 10000, 90000, 90000, 500, 20000);

            // _fakeTdItem = Substitute.For<ITransponderDataItem>();

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

        //[Test]
        //public void TestTostring()
        //{
        //    string TestString =
        //        "Tag:\t\tATR423\nX coordinate:\t39045 meters\nY coordinate:\t12932 meters\nAltitude:\t14000 meters\nTimestamp:\tJune 10, 2015 21:34:56 789\n";
        //    string testData = "ATR423;39045;12932;14000;20151006213456789";

        //   ITransponderDataItem  _fakeTdItem = _fakeDecoder.convertData(testData);
           

        //    Assert.That(TestString, Is.EqualTo(_fakeTdItem.ToString()));
        //    //Assert.AreSame(TestString, _fakeTdItem.ToString());
        //    //StringAssert.AreEqualIgnoringCase(TestString, _fakeTdItem.ToString());
        //}


        
        [TestCase(19999, 89999, 89999)]
        [TestCase(501, 50000, 50000)]
        public void TestThatAircraftIsInsideAirspace(int a, int b, int c)
        {
            ITransponderDataItem td = new TransponderDataItem(){Altitude = a, XCoordinate = b, YCoordinate = c};

            Assert.That(_fakeAirspace.IsAircraftInside(td), Is.True);
        }
        
        [Test]
        public void TestAMSController()
        {
            
        }
    }
}
