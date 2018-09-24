using System;
using System.Collections.Generic;
using System.Linq;

namespace CarModel.Car
{
    public class OnBoardComputer : IOnBoardComputer
    {
        #region [ Fields ]
        private IDrivingProcessor _drivingProcessor;
        private readonly IFuelTank _fuelTank;
        private int _currentTripTime = 0;
        private int _currentTripDistance = 0;
        private int _totalTripTime = 0;
        private int _totalDrivenTime = 0;
        private int _tripDrivenTime = 0;
        private int _totalTripDistance = 0;
        private int _totalRealTime = 0;
        private double _totalConsumption = 0;
        private double _tripConsumption = 0;
        private int _tripAverageSpeed = 0;
        private int _totalAverageSpeed = 0;
        private double _tripAverageConsumptionByDistance = 0;
        private double _totalAverageConsumptionByDistance = 0;
        private List<double> _consumptionInLast100Sec = new List<double>();
        #endregion

        #region [ Constructor ]
        public OnBoardComputer(IDrivingProcessor drivingProcessor, IFuelTank fuelTank)
        {
            _drivingProcessor = drivingProcessor;
            _fuelTank = fuelTank;
            Enumerable.Range(0, 100).ToList().ForEach(c => _consumptionInLast100Sec.Add(4.8));
        }
        #endregion

        #region [ Properties ]
        public int TripRealTime { get => _totalTripTime; }

        public int TripDrivingTime { get => _tripDrivenTime; }

        public int TripDrivenDistance { get => _totalTripDistance; }

        public int TotalRealTime { get => _totalRealTime; }

        public int TotalDrivingTime { get => _totalDrivenTime; }

        public int TotalDrivenDistance { get => _totalTripDistance; }

        public double TripAverageSpeed { get => _tripAverageSpeed / _tripDrivenTime; }

        public double TotalAverageSpeed { get => _totalAverageSpeed / _totalDrivenTime; }

        public double ActualConsumptionByTime
        {
            get
            {
                if (_currentTripTime == 0)
                {
                    return 0;
                }
                return _drivingProcessor.ActualConsumption;
            }
        }

        public double ActualConsumptionByDistance
        {
            get
            {
                if (_currentTripDistance == 0)
                {
                    return double.NaN;
                }

                if (_drivingProcessor.ActualSpeed == 0)
                {
                    return 0;
                }
                return _drivingProcessor.ActualConsumption * 100000 / _drivingProcessor.ActualSpeed * 3.6;

            }
        }

        public double TripAverageConsumptionByTime
        {
            get
            {
                if (_currentTripTime == 0)
                {
                    return 0;
                }
                return _tripConsumption / _currentTripTime;
            }
        }

        public double TotalAverageConsumptionByTime
        {
            get
            {
                if (_currentTripTime == 0)
                {
                    return 0;
                }
                return _tripConsumption / _currentTripTime;
            }
        }

        public double TripAverageConsumptionByDistance
        {
            get
            {
                if (_totalDrivenTime == 0)
                {
                    return 0;
                }
                return _tripAverageConsumptionByDistance / _totalDrivenTime;
            }
        }

        public double TotalAverageConsumptionByDistance
        {
            get
            {
                if (_totalDrivenTime == 0)
                {
                    return 0;
                }
                return _tripAverageConsumptionByDistance / _totalDrivenTime;
            }
        }

        public int EstimatedRange
        {
            get
            {
                return (int)Math.Round(_fuelTank.FillLevel / (_consumptionInLast100Sec.Sum() / 100) / 36);
            }
        }

        public int ActualSpeed { get => _drivingProcessor.ActualSpeed; }

        #endregion

        #region [ Methods ]
        public void ElapseSecond()
        {
            _currentTripTime++;
            _tripAverageSpeed += ActualSpeed;
            _totalAverageSpeed += ActualSpeed;
            _currentTripDistance += ActualSpeed;
            _totalRealTime++;
            if (ActualSpeed != 0)
            {
                _totalDrivenTime++;
                _tripDrivenTime++;
                _totalConsumption += _drivingProcessor.ActualConsumption;
                _tripConsumption += _drivingProcessor.ActualConsumption;
                _tripAverageConsumptionByDistance += _drivingProcessor.ActualConsumption * 100000 / _drivingProcessor.ActualSpeed * 3.6;
                _totalAverageConsumptionByDistance += _drivingProcessor.ActualConsumption * 100000 / _drivingProcessor.ActualSpeed * 3.6;
                _consumptionInLast100Sec.RemoveAt(0);
                _consumptionInLast100Sec.Add(_drivingProcessor.ActualConsumption);
            }

            _totalTripTime++;
            _totalTripDistance += ActualSpeed;
        }

        public void TotalReset()
        {
            _currentTripTime = 0;
            _currentTripDistance = 0;
            _totalTripTime = 0;
            _totalTripDistance = 0;
            _totalConsumption = 0;
            _tripConsumption = 0;
            _totalAverageSpeed = 0;
            _tripAverageSpeed = 0;
        }

        public void TripReset()
        {
            _currentTripDistance = 0;
            _currentTripDistance = 0;
            _tripConsumption = 0;
            _tripAverageSpeed = 0;
            _tripDrivenTime = 0;
        }
        #endregion

    }
}
