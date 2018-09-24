using System;

namespace CarModel.Car
{
    public class OnBoardComputerDisplay : IOnBoardComputerDisplay
    {

        private IOnBoardComputer _onBoardComputer;

        public OnBoardComputerDisplay(IOnBoardComputer onBoardComputer)
        {
            _onBoardComputer = onBoardComputer;
        }

        public int TripRealTime { get => _onBoardComputer.TripRealTime; }

        public int TripDrivingTime { get => _onBoardComputer.TripDrivingTime; }

        public double TripDrivenDistance { get => Math.Round((double)_onBoardComputer.TripDrivenDistance / 1000 / 3.6, 2); }

        public int TotalRealTime { get => _onBoardComputer.TotalRealTime; }

        public int TotalDrivingTime { get => _onBoardComputer.TotalDrivingTime; }

        public double TotalDrivenDistance { get => Math.Round((double)_onBoardComputer.TotalDrivenDistance / 1000 / 3.6, 2); }

        public int ActualSpeed { get => _onBoardComputer.ActualSpeed; }

        public double TripAverageSpeed { get => _onBoardComputer.TripAverageSpeed; }

        public double TotalAverageSpeed { get => _onBoardComputer.TotalAverageSpeed; }

        public double ActualConsumptionByTime { get => _onBoardComputer.ActualConsumptionByTime; }

        public double ActualConsumptionByDistance { get => Math.Round(_onBoardComputer.ActualConsumptionByDistance, 1); }

        public double TripAverageConsumptionByTime { get => Math.Round(_onBoardComputer.TripAverageConsumptionByTime, 4); }

        public double TotalAverageConsumptionByTime { get => Math.Round(_onBoardComputer.TotalAverageConsumptionByTime, 4); }

        public double TripAverageConsumptionByDistance { get => Math.Round(_onBoardComputer.TripAverageConsumptionByDistance, 1); }

        public double TotalAverageConsumptionByDistance { get => Math.Round(_onBoardComputer.TotalAverageConsumptionByDistance, 1); }

        public int EstimatedRange { get => _onBoardComputer.EstimatedRange; }

        public void TotalReset()
        {
            _onBoardComputer.TotalReset();
        }

        public void TripReset()
        {
            _onBoardComputer.TripReset();
        }
    }
}
