using System;

namespace CarModel.Car
{
    public class Car : ICar
    {
        #region [ Fields ]
        public IFuelTankDisplay fuelTankDisplay;

        private readonly IEngine engine;

        private readonly IFuelTank fuelTank;

        public IDrivingInformationDisplay drivingInformationDisplay;

        private readonly IDrivingProcessor drivingProcessor;

        public IOnBoardComputerDisplay onBoardComputerDisplay;

        private readonly IOnBoardComputer onBoardComputer;
        #endregion

        #region [ Constructor ]
        public Car(double fuelLevel, int maxAcceleration)
        {
            fuelLevel = Math.Max(Math.Min(fuelLevel, 60), 0);
            fuelTank = new FuelTank(fuelLevel);
            engine = new Engine(fuelTank);
            fuelTankDisplay = new FuelTankDisplay(fuelTank);
            drivingProcessor = new DrivingProcessor(engine, maxAcceleration);
            onBoardComputer = new OnBoardComputer(drivingProcessor, fuelTank);            
            drivingInformationDisplay = new DrivingInformationDisplay(drivingProcessor);
            onBoardComputerDisplay = new OnBoardComputerDisplay(onBoardComputer);            
        }

        public Car()
            : this(20)
        {
        }

        public Car(double fuelLevel)
            : this(fuelLevel, 10)
        {
        }
        #endregion

        #region [ Properties ]
        public bool EngineIsRunning
        {
            get
            {
                return engine.IsRunning;
            }
        }
        #endregion

        #region [ Methods ]
        public void EngineStart()
        {
            if ((!engine.IsRunning) && (fuelTank.FillLevel > 0))
            {
                engine.Start();
                onBoardComputer.TripReset();
                drivingProcessor.EngineStart();
                onBoardComputer.ElapseSecond();
            }
        }

        public void EngineStop()
        {
            if (engine.IsRunning)
            {
                onBoardComputer.TripReset();
                drivingProcessor.EngineStop();
                onBoardComputer.ElapseSecond();
                engine.Stop();
            }
        }

        public void Refuel(double liters)
        {
            fuelTank.Refuel(liters);
        }

        public void RunningIdle()
        {
            drivingProcessor.ReduceSpeed(0);
            if (engine.IsRunning)
            {
                onBoardComputer.ElapseSecond();
            }            
        }

        public void BrakeBy(int speed)
        {
            drivingProcessor.ReduceSpeed(speed);
            if (engine.IsRunning)
            {
                onBoardComputer.ElapseSecond();
            }
        }

        public void Accelerate(int speed)
        {
            drivingProcessor.IncreaseSpeedTo(speed);
            if (engine.IsRunning)
            {
                onBoardComputer.ElapseSecond();
            }
        }

        public void FreeWheel()
        {
            drivingProcessor.ReduceSpeed(1);
            if (engine.IsRunning)
            {
                onBoardComputer.ElapseSecond();
            }
        }
    }
    #endregion
}
