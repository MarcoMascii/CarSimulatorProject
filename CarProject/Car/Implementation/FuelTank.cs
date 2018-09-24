using System;

namespace CarModel.Car
{
    public class FuelTank : IFuelTank
    {
        #region [ Fields ]
        private double _fillLevel;

        private double _tankSize = 60;
        #endregion

        #region [ Constructor ]
        public FuelTank()
            : this(20)
        {
        }

        public FuelTank(double fillLevel)
        {
            if (fillLevel < 0)
            {
                fillLevel = 0;
            }
            if (fillLevel > _tankSize)
            {
                fillLevel = _tankSize;
            }

            _fillLevel = fillLevel;
        }
        #endregion

        #region [ Properties ]
        public double FillLevel { get => _fillLevel; }

        public bool IsOnReserve { get=> _fillLevel < 5; }

        public bool IsComplete { get => _fillLevel == _tankSize; }
        #endregion

        #region [ Methods ]
        public void Consume(double liters)
        {
            _fillLevel -= liters;
            _fillLevel = Math.Round(_fillLevel, 10);

            if (_fillLevel < 0)
            {
                _fillLevel = 0;
            }
        }

        public void Refuel(double liters)
        {
            _fillLevel += liters;
            if (_fillLevel > 60)
            {
                _fillLevel = 60;
            }
        }
        #endregion
    }

}
