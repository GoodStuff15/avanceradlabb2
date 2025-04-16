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

        public async Task<Car> Driving()
        {
            Car.Start();

            while(DistanceTravelled < 5000)
            {
                DistanceTravelled += KmhConverter(Car.CurrentSpeed);
                await Task.Delay(1000);
                Console.WriteLine($"Driving: {DistanceTravelled}" + Car.Name);

            }
            return Car;
        }

        public int KmhConverter(int kmh)
        {
            double Mps = kmh / 3.6;

            return (int)Mps;
        }


    }
}
