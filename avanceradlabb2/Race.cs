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
            Console.WriteLine("Race is over");
        }

        public void AddToLanes()
        {
            int lane = 1;
            foreach(var car in Participants)
            {
                Lanes.Add(new Lane(lane, RaceDistance, car));
                
                Console.WriteLine("Add lane " + lane);
                lane++;
            }
        }

        public void StartMessage()
        {
            Console.WriteLine("3");
            Thread.Sleep(100);
            Console.WriteLine("2");
            Thread.Sleep(100);
            Console.WriteLine("1");
            Thread.Sleep(100);
            Console.WriteLine("AND THEY'RE OFF!");
        }

        public async Task StartCarsAsync()
        {
            //Task<Car> task1 = Lanes[0].Driving();
            //Task<Car> task2 = Lanes[1].Driving();
            var drivingCars = new List<Task<Car>>();

            foreach (var lane in Lanes)
            {
                Console.WriteLine($"Adding {lane.LaneNumber} to tasks ");
                drivingCars.Add(lane.Driving());
            }
           
            while(drivingCars.Count > 0)
            {
                Task<Car> finished = await Task.WhenAny(drivingCars);

                await finished;

                drivingCars.Remove(finished);      
            }  
            Console.WriteLine("Race stop");
        }
    }
}
