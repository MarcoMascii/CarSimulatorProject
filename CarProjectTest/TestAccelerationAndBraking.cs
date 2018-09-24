using System;
using System.Linq;
using CarModel.Car;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarProjectTest
{
    [TestClass]
    public class TestAccelerationAndBraking
    {
        [TestMethod]
        public void TestStartSpeed()
        {
            var car = new Car();

            car.EngineStart();

            Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestFreeWheelSpeed()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            Assert.AreEqual(100, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");

            car.FreeWheel();
            car.FreeWheel();
            car.FreeWheel();

            Assert.AreEqual(97, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestAccelerateBy10()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            car.Accelerate(160);
            Assert.AreEqual(110, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
            car.Accelerate(160);
            Assert.AreEqual(120, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
            car.Accelerate(160);
            Assert.AreEqual(130, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
            car.Accelerate(160);
            Assert.AreEqual(140, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
            car.Accelerate(145);
            Assert.AreEqual(145, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestBraking()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            car.BrakeBy(20);

            Assert.AreEqual(90, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");

            car.BrakeBy(10);

            Assert.AreEqual(80, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestConsumptionSpeedUpTo30()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);

            Assert.AreEqual(0.98, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestMotorStartAndStop()
        {
            var car = new Car();

            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");

            car.EngineStart();

            Assert.IsTrue(car.EngineIsRunning, "Engine should be running.");

            car.EngineStop();

            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
        }

        [TestMethod]
        public void TestMotorStartAgainAndStopAgain()
        {
            var car = new Car();

            car.EngineStart();

            car.EngineStart();

            Assert.IsTrue(car.EngineIsRunning, "Engine should be running.");

            car.EngineStop();

            car.EngineStop();

            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
        }

        [TestMethod]
        public void TestMotorDoesntStartWithEmptyFuelTank()
        {
            var car = new Car(0);

            car.EngineStart();

            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
        }

        [TestMethod]
        public void TestFuelConsumptionOnIdle()
        {
            var car = new Car(1);

            car.EngineStart();

            Enumerable.Range(0, 3000).ToList().ForEach(s => car.RunningIdle());

            Assert.AreEqual(0.10, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestEngineStopsCauseOfNoFuelExactly()
        {
            var car = new Car(6);

            car.EngineStart();

            Enumerable.Range(0, 20000).ToList().ForEach(s => car.RunningIdle());

            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
        }

        [TestMethod]
        public void TestEngineStopsCauseOfNoFuelOver()
        {
            var car = new Car(1);

            car.EngineStart();

            Enumerable.Range(0, 10000).ToList().ForEach(s => car.RunningIdle());

            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
        }

        [TestMethod]
        public void TestNoConsumptionWhenEngineNotRunning()
        {
            var car = new Car(1);

            Enumerable.Range(0, 1000).ToList().ForEach(s => car.RunningIdle());

            Assert.AreEqual(1, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestFuelTankDisplayIsNotComplete()
        {
            var car = new Car();

            Assert.IsFalse(car.fuelTankDisplay.IsComplete, "Fuel tank must be not complete!");
        }

        [TestMethod]
        public void TestFuelTankDisplayIsComplete()
        {
            var car = new Car(60);

            Assert.IsTrue(car.fuelTankDisplay.IsComplete, "Fuel tank must be complete!");
        }

        [TestMethod]
        public void TestFuelTankDisplayIsNotOnReserve()
        {
            var car = new Car();

            Assert.IsFalse(car.fuelTankDisplay.IsOnReserve, "Fuel tank must be not on reserve!");
        }

        [TestMethod]
        public void TestFuelTankDisplayIsOnReserve()
        {
            var car = new Car(4);

            Assert.IsTrue(car.fuelTankDisplay.IsOnReserve, "Fuel tank must be on reserve!");
        }

        [TestMethod]
        public void TestRefuel()
        {
            var car = new Car(5);

            car.Refuel(40);

            Assert.AreEqual(45, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestRefuelOverMaximum()
        {
            var car = new Car(5);

            car.Refuel(80);

            Assert.AreEqual(60, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestNoNegativeFuelLevelAllowed()
        {
            var car = new Car(-5);

            Assert.AreEqual(0, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestFuelLevelAllowedUpTo60()
        {
            var car = new Car(65);

            Assert.AreEqual(60, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void Car1RandomTests()
        {
            var rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                var car1 = new Car(5);

                var refuelLiter = rand.Next(60);

                car1.Refuel(refuelLiter);

                double expectedFuelLevel = Math.Min(5 + refuelLiter, 60);
                Assert.AreEqual(expectedFuelLevel, car1.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");

                var car2 = new Car(5);
                car2.EngineStart();

                var runningIdleSeconds = rand.Next(7);

                Enumerable.Range(0, runningIdleSeconds * 10001 / 3).ToList().ForEach(s => { car2.RunningIdle(); });

                expectedFuelLevel = Math.Max(5 - runningIdleSeconds, 0);

                Assert.AreEqual(expectedFuelLevel, car2.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
                if (expectedFuelLevel == 0)
                {
                    Assert.IsFalse(car2.EngineIsRunning, "Engine could not be running.");
                }
            }
        }

        [TestMethod]
        public void TestFreeWheelNoSpeedReduceWhenNoMoving()
        {
            var car = new Car();

            car.EngineStart();

            Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");

            car.FreeWheel();

            Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }


        [TestMethod]
        public void TestBrakeOnlyOver0()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 11).ToList().ForEach(c => car.BrakeBy(10));

            Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestAccelerateOnlyUntil250()
        {
            var car = new Car(20, 20);

            car.EngineStart();

            Enumerable.Range(0, 13).ToList().ForEach(c => car.Accelerate(260));

            Assert.AreEqual(250, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestAccelerateLowerThanActualSpeed()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            car.Accelerate(30);

            Assert.AreEqual(99, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestFreeWheelEndingAtZero()
        {
            var car = new Car();

            car.EngineStart();

            car.Accelerate(5);

            Enumerable.Range(0, 6).ToList().ForEach(s => car.FreeWheel());

            Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestNoAccelerationWhenEngineNotRunning()
        {
            var car = new Car();

            car.Accelerate(5);

            Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        }

        [TestMethod]
        public void TestConsumptionSpeedUpTo80()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            car.Accelerate(80);
            car.Accelerate(80);
            car.Accelerate(80);
            car.Accelerate(80);

            Enumerable.Range(0, 17).ToList().ForEach(s => car.Accelerate(80));

            Assert.AreEqual(0.97, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestConsumptionSpeedUpTo120()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            car.Accelerate(120);
            car.Accelerate(120);
            car.Accelerate(120);
            car.Accelerate(120);
            car.Accelerate(120);
            car.Accelerate(120);

            Enumerable.Range(0, 20).ToList().ForEach(s => car.Accelerate(120));

            Assert.AreEqual(0.95, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestConsumptionSpeedUpTo250()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            car.Accelerate(250); // 20
            car.Accelerate(250); // 40
            car.Accelerate(250); // 60
            car.Accelerate(250); // 80
            car.Accelerate(250); // 100
            car.Accelerate(250); // 120
            car.Accelerate(250); // 140
            car.Accelerate(250); // 160
            car.Accelerate(250); // 180
            car.Accelerate(250); // 200
            car.Accelerate(250); // 220
            car.Accelerate(250); // 240
            car.Accelerate(250); // 250

            Assert.AreEqual(0.97, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void TestConsumptionLeadsToStopEngine()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            car.Accelerate(250); // 20
            car.Accelerate(250); // 40
            car.Accelerate(250); // 60
            car.Accelerate(250); // 80
            car.Accelerate(250); // 100
            car.Accelerate(250); // 120
            car.Accelerate(250); // 140
            car.Accelerate(250); // 160
            car.Accelerate(250); // 180
            car.Accelerate(250); // 200
            car.Accelerate(250); // 220
            car.Accelerate(250); // 240
            car.Accelerate(250); // 250

            Enumerable.Range(0, 325).ToList().ForEach(s => car.Accelerate(250));

            Assert.AreEqual(0, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
            Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
        }

        [TestMethod]
        public void TestConsumptionAsRunIdleWhenFreeWheelingAt0()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            Enumerable.Range(0, 200).ToList().ForEach(s => car.FreeWheel());

            Assert.AreEqual(0.94, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
        }

        [TestMethod]
        public void Car2RandomTests()
        {
            var rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                var maxAcceleration = rand.Next(5, 20);
                var expectedSpeed = 0;
                double expectedFuelLevel = 20;

                var car = new Car(20, maxAcceleration);

                car.EngineStart();

                Enumerable.Range(0, 10).ToList().ForEach(s =>
                {
                    car.Accelerate(250);
                    expectedSpeed += maxAcceleration;
                    expectedFuelLevel -= GetConsumption(expectedSpeed);
                });

                var brakeBySpeed = rand.Next(5, 16);

                car.BrakeBy(brakeBySpeed);

                expectedSpeed -= Math.Min(brakeBySpeed, 10);

                var freeWheelSeconds = rand.Next(10, 20);
                Enumerable.Range(0, freeWheelSeconds).ToList().ForEach(c =>
                {
                    car.FreeWheel();
                    if (expectedSpeed > 0)
                    {
                        expectedSpeed--;
                    }
                    if (expectedSpeed == 0)
                    {
                        expectedFuelLevel -= 0.0003;
                    }
                });

                var accelerateSpeed = rand.Next(5, 12);

                car.Accelerate(expectedSpeed + accelerateSpeed);

                expectedSpeed = expectedSpeed + Math.Min(maxAcceleration, accelerateSpeed);

                Assert.AreEqual(expectedSpeed, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
                Assert.AreEqual(Math.Round(expectedFuelLevel, 2), car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
            }
        }

        private double GetConsumption(int speed)
        {
            double consumption = 0.0020;

            if ((speed > 61) && (speed <= 100))
            {
                consumption = 0.0014;
            }
            if ((speed > 141) && (speed <= 200))
            {
                consumption = 0.0025;
            }
            if ((speed > 201) && (speed <= 250))
            {
                consumption = 0.0030;
            }

            return consumption;
        }
    }
}
