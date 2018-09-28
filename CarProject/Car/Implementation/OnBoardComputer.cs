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
        private int _currentTripDistance = 0;
        private int _tripRealTime = 0;
        private int _totalDrivingTime = 0;
        private int _tripDrivingTime = 0;
        private int _tripDrivenDistance = 0;
        private int _totalRealTime = 0;
        private double _totalConsumption = 0;
        private double _tripConsumption = 0;
        private double _tripAverageSpeed = 0;
        private double _totalAverageSpeed = 0;
        private int _totalDrivenDistance = 0;
        private double _tripAverageConsumptionByDistance = 0;
        private double _totalAverageConsumptionByDistance = 0;
        private double _tripAverageConsumptionByTime = 0;
        private double _totalAverageConsumptionByTime = 0;
        private List<double> _consumptionInLast100Sec = new List<double>();
        #endregion

        #region [ Constructor ]
        public OnBoardComputer(IDrivingProcessor drivingProcessor, IFuelTank fuelTank)
        {
            _drivingProcessor = drivingProcessor;
            _fuelTank = fuelTank;
            Enumerable.Range(0, 100).ToList().ForEach(c => _consumptionInLast100Sec.Add(0.048));
        }
        #endregion

        #region [ Properties ]
        public int TripRealTime { get => _tripRealTime; }

        public int TripDrivingTime { get => _tripDrivingTime; }

        public int TripDrivenDistance { get => _tripDrivenDistance; }

        public int TotalRealTime { get => _totalRealTime; }

        public int TotalDrivingTime { get => _totalDrivingTime; }

        public int TotalDrivenDistance { get => _totalDrivenDistance; }

        public double TripAverageSpeed { get => _tripAverageSpeed / _tripDrivingTime; }

        public double TotalAverageSpeed { get => _totalAverageSpeed / _totalDrivingTime; }

        public double ActualConsumptionByTime
        {
            get
            {
                if (_tripRealTime == 0)
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
                if (_tripRealTime == 0)
                {
                    return 0;
                }
                return _tripConsumption / _tripRealTime;
            }
        }

        public double TotalAverageConsumptionByTime
        {
            get
            {
                if (_totalRealTime == 0)
                {
                    return 0;
                }
                return _totalConsumption / _totalRealTime;
            }
        }

        public double TripAverageConsumptionByDistance
        {
            get
            {
                if (_totalDrivingTime == 0)
                {
                    return 0;
                }
                return _tripAverageConsumptionByDistance / _totalDrivingTime;
            }
        }

        public double TotalAverageConsumptionByDistance
        {
            get
            {
                if (_totalDrivingTime == 0)
                {
                    return 0;
                }
                return _tripAverageConsumptionByDistance / _totalDrivingTime;
            }
        }

        public int EstimatedRange
        {
            get
            {
                return (int)Math.Round((_fuelTank.FillLevel / _consumptionInLast100Sec.Average()));                
            }
        }

        public int ActualSpeed { get => _drivingProcessor.ActualSpeed; }

        #endregion

        #region [ Methods ]
        public void ElapseSecond()
        {
            _tripAverageSpeed += ActualSpeed;
            _totalAverageSpeed += ActualSpeed;
            _currentTripDistance += ActualSpeed;
            _totalRealTime++;
            _tripRealTime++;

            if (ActualSpeed != 0)
            {
                _totalDrivingTime++;
                _tripDrivingTime++;
                _tripConsumption += _drivingProcessor.ActualConsumption;
                _totalConsumption += _drivingProcessor.ActualConsumption;
                _tripAverageConsumptionByDistance += _drivingProcessor.ActualConsumption * 100000 / _drivingProcessor.ActualSpeed * 3.6;
                _totalAverageConsumptionByDistance += _drivingProcessor.ActualConsumption * 100000 / _drivingProcessor.ActualSpeed * 3.6;
                _tripAverageConsumptionByTime += ActualConsumptionByTime;
                _totalAverageConsumptionByTime += ActualConsumptionByTime;
                _consumptionInLast100Sec.RemoveAt(0);
                _consumptionInLast100Sec.Add(_drivingProcessor.ActualConsumption / _drivingProcessor.ActualSpeed * 3600);
            }
            _totalDrivenDistance += ActualSpeed;
            _tripDrivenDistance += ActualSpeed;
        }

        public void TotalReset()
        {
            _totalDrivenDistance = 0;
            _totalConsumption = 0;
            _totalAverageSpeed = 0;
            _totalDrivingTime = 0;
            _totalRealTime = 0;
            _totalAverageConsumptionByDistance = 0;
            _totalAverageConsumptionByTime = 0;

        }

        public void TripReset()
        {
            _tripAverageConsumptionByTime = 0;
            _currentTripDistance = 0;
            _currentTripDistance = 0;
            _tripConsumption = 0;
            _tripAverageSpeed = 0;
            _tripDrivingTime = 0;
            _tripRealTime = 0;
            _tripDrivenDistance = 0;
            _tripAverageConsumptionByDistance = 0;
            _totalAverageConsumptionByTime = 0;
        }
        #endregion

    }
}
