using System;

namespace CarModel.Car
{
    public class FuelTankDisplay : IFuelTankDisplay
    {
        #region [ Fields ]
        private IFuelTank _fuelTank;
        #endregion

        #region [ Constructor ]
        public FuelTankDisplay(IFuelTank fuelTank)
        {
            _fuelTank = fuelTank;
        }
        #endregion

        #region [ Properties ]
        public double FillLevel { get => Math.Round(_fuelTank.FillLevel, 2); }

        public bool IsOnReserve { get => _fuelTank.IsOnReserve; }

        public bool IsComplete { get => _fuelTank.IsComplete; }
        #endregion

    }
}
