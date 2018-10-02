﻿using System;
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
        private Monitor uutMonitor;
        

        [SetUp]
        public void setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new ATMhandin2.Classes.TransponderReceiver(_fakeTransponderReceiver);
            uutMonitor = new Monitor();
        }

        [Test]
        public void TestLongtitude()
        {
            Assert.That(uutMonitor.longTitude(4, 2, 4, 4), Is.EqualTo(2));
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
    }
}
