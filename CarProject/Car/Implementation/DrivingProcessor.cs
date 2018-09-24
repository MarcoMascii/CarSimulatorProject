using System;

namespace CarModel.Car
{
    public class DrivingProcessor : IDrivingProcessor
    {
        #region [ Fields ]
        private const int brakingSpeed = 10;

        private const int maxSpeed = 250;

        private readonly IEngine _engine;

        private readonly int _maxAcceleration;

        private int _actualSpeed = 0;
        private double _actualConsumption = 0;

        #endregion

        #region [ Constructor ]
        public DrivingProcessor(IEngine engine, int maxAcceleration)
        {
            if (maxAcceleration < 5)
            {
                maxAcceleration = 5;
            }
            if (maxAcceleration > 20)
            {
                maxAcceleration = 20;
            }
            _engine = engine;
            _maxAcceleration = maxAcceleration;
        }
        #endregion

        #region [ Properties ]
        public int ActualSpeed { get => _actualSpeed; }
        public double ActualConsumption { get => _actualConsumption; }

        #endregion

        #region [ Methods ]
        public void EngineStart()
        {
            _actualSpeed = 0;
            _actualConsumption = 0;
        }

        public void EngineStop()
        {
            _actualSpeed = 0;
            _actualConsumption = 0;
        }

        public void IncreaseSpeedTo(int speed)
        {
            if (!_engine.IsRunning)
            {
                return;
            }

            if (speed < _actualSpeed)
            {
                _actualSpeed--;
            }

            if (_actualSpeed < speed)
            {
                _actualSpeed = Math.Min(speed, _actualSpeed + _maxAcceleration);
            }

            if (_actualSpeed > maxSpeed)
            {
                _actualSpeed = maxSpeed;
            }

            _engine.Consume(GetConsumption());
        }

        public void ReduceSpeed(int speed)
        {
            if (!_engine.IsRunning)
            {
                return;
            }

            _actualConsumption = 0;
            _actualSpeed -= Math.Min(speed, brakingSpeed);

            if (_actualSpeed < 0)
            {
                _actualSpeed = 0;
            }

            if (_actualSpeed == 0)
            {
                _engine.Consume(0.0003);
                _actualConsumption = 0.0003;
            }
            
        }

        private double GetConsumption()
        {
            if (!_engine.IsRunning)
            {
                return 0;
            }

            double consumption = 0.0020;

            if ((_actualSpeed > 61) && (_actualSpeed <= 100))
            {
                consumption = 0.0014;
            }
            if ((_actualSpeed > 141) && (_actualSpeed <= 200))
            {
                consumption = 0.0025;
            }
            if ((_actualSpeed > 201) && (_actualSpeed <= 250))
            {
                consumption = 0.0030;
            }

            if (_actualSpeed == 0)
            {
                consumption = 0.0003;
            }

            _actualConsumption = consumption;
            return consumption;
        }

        #endregion

    }
}
