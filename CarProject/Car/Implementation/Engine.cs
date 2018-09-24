namespace CarModel.Car
{
    public class Engine : IEngine
    {
        #region [ Fields ]
        private bool _isRunning = false;
        private IFuelTank _fuelTank;
        #endregion

        #region [ Constructor ]
        public Engine(IFuelTank fuelTank)
        {
            _fuelTank = fuelTank;
        }
        #endregion

        #region [ Properties ]
        public bool IsRunning { get => _isRunning; }
        #endregion

        #region [ Methods ]
        public void Consume(double liters)
        {
            if (_isRunning)
            {
                _fuelTank.Consume(liters);

                if (_fuelTank.FillLevel == 0)
                {
                    Stop();
                }
            }
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }
        #endregion
    }
}
