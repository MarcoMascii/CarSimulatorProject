namespace CarModel.Car
{
    public interface ICar
    {
        void BrakeBy(int speed);

        void Accelerate(int speed); 

        bool EngineIsRunning { get; }

        void EngineStart();

        void EngineStop();

        void Refuel(double liters);

        void FreeWheel(); 

        void RunningIdle();
    }
}
