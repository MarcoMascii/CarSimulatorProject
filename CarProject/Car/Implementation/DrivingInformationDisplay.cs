namespace CarModel.Car
{
    public class DrivingInformationDisplay : IDrivingInformationDisplay
    {
        #region [ Fields ]
        private IDrivingProcessor _drivingProcessor;
        #endregion

        #region [ Constructor ]
        public DrivingInformationDisplay(IDrivingProcessor drivingProcessor)
        {
            _drivingProcessor = drivingProcessor;
        }
        #endregion

        public int ActualSpeed { get => _drivingProcessor.ActualSpeed; }
    }
}
