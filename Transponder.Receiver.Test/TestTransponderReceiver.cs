using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            private DateTime tmpTime1;
            private DateTime tmpTime2;
            
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
                    ("Tag:\t\t\tMUH120\nX coordinate:\t\t32000 meters\nY coordinate:\t\t70000 meters\nAltitude:\t\t2500 meters\nTimestamp:\t\tjanuar 01, 0001 00:00:00 000\nCompassCourse:\t\t0\nHorizontalVelocity:\t0\n");
            
                Assert.That(_uut.ToString(), Is.EqualTo(TestString));
            }

            [Test]
            public void TestUpdate()
            {
                string TestString =
                    ("Tag:\t\t\tMUH120\nX coordinate:\t\t15000 meters\nY coordinate:\t\t20000 meters\nAltitude:\t\t700 meters\nTimestamp:\t\tjuli 09, 2018 20:40:10 902\nCompassCourse:\t\t18\nHorizontalVelocity:\t0\n");
                
                ITransponderDataItem td = new TransponderDataItem();
                
                td.XCoordinate = 15000;
                td.YCoordinate = 20000;
                td.Altitude = 700;
                td.TimeStamp = new DateTime(2018,07,09,20,40,10,902);
                
                _uut.Update(td);
                
                Assert.That(_uut.ToString(), Is.EqualTo(TestString));
            }

            [TestCase(10000,12000,50000,55000, 21.8014094863518117702448660869436645)]
            [TestCase(90000,90000,90000,55000, 21.8014094863518117702448660869436645)]
            public void TestCalculateAngle(double a, double b, double c, double d, double e)
            {
                Assert.That(_uut.CalculateAngle(a,b,c,d), Is.EqualTo(e));
            }

        

            [TestCase(14000,12000,57000,52000)]
            public void TestCalculateVelocity(int a, int b, int c, int d)
            {
                tmpTime1 = new DateTime(2018, 07, 09, 20, 40, 10, 902);
                tmpTime2 = new DateTime(2018, 07, 09, 20, 40, 20, 902);
            
                Assert.That(_uut.CalculateVelocity(a,b,c,d,tmpTime1,tmpTime2), Is.EqualTo(-538.0));

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
                
                // Updating the coordinates for the plane so that it is now out of the airspace.
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
                //_fakeTransponderDataItem = Substitute.For<ITransponderDataItem>();

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

    public class TestCoalitionAvoidanceSystem
    {
        //private ITransponderDataItem _fakeTransponderDataItem;
        private CoalitionAvoidanceSystem _uut;
        private AMSController _amsController;
        private ITransponderReceiver _fakeTransponderReceiver;
        
        [SetUp]
        public void Setup()
        {
            //_fakeTransponderDataItem = Substitute.For<ITransponderDataItem>();

            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            
            _amsController = new AMSController(new TransponderReceiverClient(_fakeTransponderReceiver));
            
            _uut = new CoalitionAvoidanceSystem(_amsController);
        }

        [TestCase(11944, 43486, 12510, 45348)]
        public void TestDistanceCalculation(double x1Coor, double y1Coor, double x2Coor, double y2Coor)
        {
            double CalculatedDistance = _uut.distanceTo(x1Coor, x2Coor, y1Coor, y2Coor);
 
            Assert.That(CalculatedDistance, Is.EqualTo(1946.124353683494707425007744333489429246012287529387513188));
        }

        [TestCase("MUH120", 11944, 43486, 12200,"LAK340", 12510, 45348, 12500)]
        [TestCase("MUH120", 11944, 43486, 12200,"LAK340", 12510, 45348, 12200)]
        [TestCase("MUH120", 11944, 43486, 12200,"LAK340", 12510, 45348, 11900)]
        [TestCase("MUH120", 11944, 43486, 12200,"LAK340", 12510, 45348, 12000)]
        [TestCase("MUH120", 10000, 13000, 12200, "LAK340", 10000, 14000, 12200)]
        [TestCase("MUH120", 10000, 14000, 12200, "LAK340", 10000, 13000, 12200)]
        [TestCase("MUH120", 15000, 15000, 12200, "LAK340", 13000, 13000, 12300)]
        public void TestCoalitionWarningTrue(string a, int b, int c, int d, string e, int f, int g, int h)
        {
            Aircraft air1 = new Aircraft(a, b, c, d);
            Aircraft air2 = new Aircraft(e, f, g, h);

            _amsController._aircraftsInsideAirspace.Add("1", air1);
            _amsController._aircraftsInsideAirspace.Add("2", air2);

            Assert.That(_uut.CoalitionWarning(), Is.EqualTo(true));

        }

        [Test]
        public void TestCoalitionWarningFalse()
        {
            Aircraft air1 = new Aircraft("MUH120", 15000, 15000, 700);
            Aircraft air2 = new Aircraft("LAK340", 77000, 77000, 2800);

            _amsController._aircraftsInsideAirspace.Add("1", air1);
            _amsController._aircraftsInsideAirspace.Add("2", air2);

            Assert.That(_uut.CoalitionWarning(), Is.EqualTo(false));

        }
    }
}
