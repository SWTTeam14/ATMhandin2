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
            private IAircraft _fakeAircraft;
            private Aircraft _uut;
            private string Tag = "MUH120";
            private int Xcoor = 32000, Ycoor = 70000, Altitude = 2500;
            
            [SetUp]
            public void Setup()
            {
                _fakeAircraft = Substitute.For<IAircraft>();

                _uut = new Aircraft(Tag, Xcoor, Ycoor, Altitude);
            }

            [Test]
            public void TestToString()
            {
                string TestString =
                    ("Tag:\t\tMUH120\nX coordinate:\t32000 meters\nY coordinate:\t70000 meters\nAltitude:\t2500 meters\nTimestamp:\tjanuar 01, 0001 00:00:00 000\n");

                Assert.That(_uut.ToString(), Is.EqualTo(TestString));
            }

            [Test]
            public void TestUpdate()
            {
                string TestString =
                    ("Tag:\t\tMUH120\nX coordinate:\t15000 meters\nY coordinate:\t20000 meters\nAltitude:\t700 meters\nTimestamp:\tjuli 09, 2018 20:40:10 902\n");

                ITransponderDataItem td = new TransponderDataItem();
                
                td.XCoordinate = 15000;
                td.YCoordinate = 20000;
                td.Altitude = 700;
                td.TimeStamp = new DateTime(2018,07,09,20,40,10,902);

                _uut.Update(td);
                
                Assert.That(_uut.ToString(), Is.EqualTo(TestString));
            }

        }

    public class TestAMSController
        {
            private IAMSController _fakeAmsController;
            private TransponderReceiverClient trc;
            private AMSController _uut;
            private ITransponderReceiver _fakeTransponderReceiver;

            [SetUp]
            public void Setup()
            {
                _fakeAmsController = Substitute.For<IAMSController>();
                _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
                trc = new TransponderReceiverClient(_fakeTransponderReceiver);
                _uut = new AMSController(trc);
            }


            [Test]
            public void Add3planesToAirspaceAndRemoveOneBecauseOutOfAirSpace()
            {
                List<string> testData = new List<string>();
                testData.Add("ATR423;39045;12932;14000;20151006213456789");
                testData.Add("BCD123;10005;85890;12000;20151006213456789");
                testData.Add("XYZ987;25059;75654;4000;20151006213456789");

                //ACT
                _fakeTransponderReceiver.TransponderDataReady +=
                    Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

                //Assert
                
                Assert.That(_uut._aircraftsInsideAirspace.Count, Is.EqualTo(3));
                

                testData[0] = "ATR423;91000;12932;14000;20151006213456789";
                _fakeTransponderReceiver.TransponderDataReady +=
                    Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

                Assert.That(_uut._aircraftsInsideAirspace.Count, Is.EqualTo(2));
            }
        }

    public class TestTransponderDataItem
        {
            private ITransponderDataItem _fakeTransponderDataItem;
            private TransponderDataItem _uut;

            [SetUp]
            public void Setup()
            {
                _fakeTransponderDataItem = Substitute.For<ITransponderDataItem>();

                _uut = new TransponderDataItem();

                _uut.Tag = "MUH120";
                _uut.XCoordinate = 15000;
                _uut.YCoordinate = 20000;
                _uut.Altitude = 700;
                _uut.TimeStamp = new DateTime(2018, 07, 09, 20, 40, 10, 902);
            }

            [Test]
            public void TestToString()
            {
            string TestString =
                ("Tag:\t\tMUH120\nX coordinate:\t15000 meters\nY coordinate:\t20000 meters\nAltitude:\t700 meters\nTimestamp:\tjuli 09, 2018 20:40:10 902\n");

            Assert.That(_uut.ToString(), Is.EqualTo(TestString));
        }
            


        }
    
}
