using System.Security.Cryptography.X509Certificates;

namespace avanceradlabb2
{

    public class Race
    {
        public string Name { get; private set; }
        public int RaceDistance { get; private set; }

        public List<Car> Participants { get; private set; } = new List<Car>();

        public string[]? RacePlacement { get; private set; }

        public List<Trouble> Troubles { get; private set; } = new List<Trouble>();

        public List<Lane> Lanes { get; private set; } = new List<Lane>();

        private bool _racing = false;

        public Race(string name, int distance) 
        {
            Name = name;
            RaceDistance = distance;

        }

        public void RaceSetup()
        {
            var trouble1 = new Trouble_Delay(15000, "You are out of gas.", 1, "Why didn't you refill before the important race! Refill takes 15 seconds.");
            var trouble2 = new Trouble_Delay(10000, "You have a flat tire.", 2, "POP! A tire explodes, you need to stop and change it! Tire change takes 10 seconds.");
            var trouble3 = new Trouble_Delay(5000, "A bird hit your windshield.", 5, "There is bird smeared all over! Stop for 5 seconds and clean it.");
            var trouble4 = new Trouble_Delay(10000, "You drove over a rock.", 1, "\"We had to stop, because it came some stone or something trough Timo's seat. Up in the asshole of Timo!\" Stop for 10 seconds to help Timo.");
            var trouble5 = new Trouble_Functionality(-1, "Your engine made a weird sound.", 10, "You slow down a bit");

            Troubles.Add(trouble1);
            Troubles.Add(trouble2);
            Troubles.Add(trouble3);
            Troubles.Add(trouble4);
            Troubles.Add(trouble5);

            Participants.Add(new Car("COOL CAR"));
            Participants.Add(new Car("Peugeot"));
            Participants.Add(new Car("Batmobilen"));
            Participants.Add(new Car("Cykel"));

            RacePlacement = new string[Participants.Count];
        }

        public async Task StartRace()
        {
            RaceSetup();
            AddToLanes();
            StartMessage();

            await StartCarsAsync();
            Results();
        }

        public void Results()
        {
            Console.WriteLine("Final standings!");
            Console.WriteLine("---------------");
            for(int i = 0; i < RacePlacement.Length; i++) 
            {
                Console.WriteLine($"{i+1}: {RacePlacement[i]}");
            }
        }
        public void AddToLanes()
        {
            int lane = 1;
            foreach(var car in Participants)
            {
                Lanes.Add(new Lane(lane, RaceDistance, car, Troubles));
                lane++;
            }
        }

        public void StartMessage()
        {
            Console.WriteLine("3");
           
            Thread.Sleep(1000);
            Console.WriteLine("2");
           
            Thread.Sleep(1000);
            Console.WriteLine("1");
            
            Thread.Sleep(1000);
         
            Console.WriteLine("AND THEY'RE OFF!");
            Thread.Sleep(1000);
        }

        public async Task StartCarsAsync()
        {
            var drivingCars = new List<Task<Lane>>();

            foreach (var lane in Lanes)
            {
                drivingCars.Add(lane.Driving());
            }
            Console.Clear();
            Console.WriteLine("Press enter to see Race Status");
            await RunRaceAsync(drivingCars);
        }

        public async Task RunRaceAsync(List<Task<Lane>> lanes)
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            var task = Task.Run(() => DetectPush(Lanes),ct);
            int placement = 0;
            while (lanes.Count > 0)
            {
                
                Task<Lane> finished = await Task.WhenAny(lanes);
                
                await finished;

                RacePlacement[placement] = finished.Result.Car.Name;
                
                if(placement == 0)
                {
                    Console.WriteLine($"{finished.Result.Car.Name} has crossed the finish line as the winner!!!! Time:");
                }
                else
                {
                    Console.WriteLine($"{finished.Result.Car.Name} has crossed the finish line! Time: ");
                }
                placement++;
                Console.WriteLine(finished.Result.Stopwatch.Elapsed.Minutes.ToString("D2") + ":" + finished.Result.Stopwatch.Elapsed.Seconds.ToString("D2") + "\n");
                lanes.Remove(finished);
                
             
            }
            tokenSource.Cancel();
           
        }


        public async Task DetectPush(List<Lane> lanes)
        {
            while(true)
            {

            var cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.Enter)
            {
                
                foreach (var lane in lanes)
                {
                    Console.WriteLine("-----");
                    Console.WriteLine($"{lane.Car.Name}: ");
                    Console.WriteLine($"Current Position: {lane.DistanceTravelled}m");
                    Console.WriteLine($"Current Speed: {lane.Car.CurrentSpeed}km/h");
                    Console.WriteLine("-----");
                }

            }

            }

        }


    }
}
