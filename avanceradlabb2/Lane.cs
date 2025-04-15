namespace avanceradlabb2
{
    public class Lane
    {
        public int LaneNumber { get; private set; }
        public int Distance { get; private set; }

        public Car Car { get; private set; }

        public int DistanceTravelled { get; set; }

        private int _troubleInterval = 10;

        private int _troubleTimer = 0;

        public Lane(int lane, int distance, Car car)
        {
            LaneNumber = lane;
            Distance = distance;
            Car = car;
            DistanceTravelled = 0;
        }

        public async Task Driving()
        {
            Car.Start();

            for (int i = 0; i < 100; i++) 
            {
                await Task.Delay(100);
                Console.WriteLine(Car.Name);
            
            }

        }
    }
}
