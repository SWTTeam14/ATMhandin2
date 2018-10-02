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

        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);
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
            _fakeTransponderReceiver.TransponderDataReady +=
                Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            //ASSERT

        }
    }

    public class TestTransponderDataItem
    {
        private ITransponderDataItem _fakeTdItem;
        private TransponderDataItem _uut;


        [SetUp]
        public void Setup()
        {
            _fakeTdItem = Substitute.For<ITransponderDataItem>();

            _uut = new TransponderDataItem();
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




        //[TestCase()]
        //public void TestUpdateAircraftPosition(int a, int b, int c)
        //{

        //    ITransponderDataItem td = new TransponderDataItem()
        //        {Altitude = a, Tag = "", TimeStamp = , XCoordinate = b, YCoordinate = c};

        //    _fakeAircraft.Update(td);
        //}

        public class TestDecoder
        {
            private IDecoder _fakeDecoder;
            private Decoder _uut;

            [SetUp]
            public void Setup()
            {
                _fakeDecoder = Substitute.For<IDecoder>();

                _uut = new Decoder();
            }

            [Test]
            public void TestConvertData()
            {
                string testData = "ATR423;39045;12932;14000;20151006213456789";

                TransponderDataItem td = _fakeDecoder.convertData(testData);

                Assert.That(_fakeDecoder.convertData(testData), Is.EqualTo(td));
            }
        }

        public class TestAirspace
        {
            private Airspace _uut;

            [SetUp]
            public void Setup()
            {
               
                _uut = new Airspace(10000, 10000, 90000, 90000, 500, 20000);
            }

            [TestCase(20000, 90000, 90000)]
            [TestCase(500, 10000, 90000)]
            [TestCase(500, 50000, 50000)]
            public void TestThatAircraftIsInsideAirspace(int a, int b, int c)
            {
                ITransponderDataItem td = new TransponderDataItem() {Altitude = a, XCoordinate = b, YCoordinate = c};

                Assert.That(_uut.IsAircraftInside(td), Is.True);
            }

            [TestCase(20001, 90000, 90000)]
            [TestCase(499, 10000, 90000)]
            [TestCase(-99, 50000, 50000)]
            [TestCase(-99, 9999, 50000)]
            [TestCase(-99, 90001, 50000)]
            [TestCase(-99, 50000, 90001)]
            public void TestThatAircraftIsNOTInsideAirspace(int a, int b, int c)
            {
                ITransponderDataItem td = new TransponderDataItem() {Altitude = a, XCoordinate = b, YCoordinate = c};

                Assert.That(_uut.IsAircraftInside(td), Is.False);
            }


        }

        public class TestAircraft
        {
            //private IAirspace _fakeAirspace;
            private Airspace _uut;
            private int South, North, West, East, Low, High;

            [SetUp]
            public void Setup()
            {
                //_fakeAirspace = Substitute.For<IAirspace>();

                _uut = new Airspace(10000, 10000, 90000, 90000, 500, 20000);
            }


        }

        public class TestAMSController
        {
            private IAMSController _fakeAmsController;
            private TransponderReceiverClient trc;
            private AMSController _uut;

            [SetUp]
            public void Setup()
            {
                _fakeAmsController = Substitute.For<IAMSController>();

                _uut = new AMSController(trc);
            }


        }

        //public class TestMonitor
        //{
        //    private IMonitor;
        //    private 
        //
        //    [SetUp]
        //    public void Setup() { }
        //
        //
        //}
    }
}
