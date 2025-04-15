using System.Security.Cryptography.X509Certificates;

namespace avanceradlabb2
{

    public class Race
    {
        public string Name { get; private set; }
        public int RaceDistance { get; private set; }

        public List<Car> Participants { get; private set; } = new List<Car>();

        public List<Trouble> Troubles { get; private set; }

        public List<Lane> Lanes { get; private set; } = new List<Lane>();

        public Race(string name, int distance) 
        {
            Name = name;
            RaceDistance = distance;
            Participants.Add(new Car("OlofCar"));
            Participants.Add(new Car("StefanCar"));
        }

        public async Task StartRace()
        {
            AddToLanes();
            StartMessage();
            await StartCarsAsync();
        }

        public void AddToLanes()
        {
            int lane = 1;
            foreach(var car in Participants)
            {
                Lanes.Add(new Lane(lane, RaceDistance, car));
                lane++;
            }
        }

        public void StartMessage()
        {
            Console.WriteLine("3");
            Thread.Sleep(500);
            Console.WriteLine("2");
            Thread.Sleep(500);
            Console.WriteLine("1");
            Thread.Sleep(500);
            Console.WriteLine("AND THEY'RE OFF!");
        }

        public async Task StartCarsAsync()
        {
            List<Task> vrroom = new List<Task>();
            foreach(var lane in Lanes)
            {
                vrroom.Add(lane.Driving());
            }

            await Task.WhenAll(vrroom);
        }
    }
}
