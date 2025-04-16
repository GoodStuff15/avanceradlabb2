using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace avanceradlabb2
{
    public class Lane
    {
        public int LaneNumber { get; private set; }
        public int Distance { get; private set; }

        public Car Car { get; private set; }

        public Dictionary<Range, Trouble> Troubles = new Dictionary<Range, Trouble>();

        public Stopwatch Stopwatch { get; private set; } = new Stopwatch();

        public int DistanceTravelled { get; set; }

        private int _troubleInterval = 10;

        private int _troubleTimer = 0;

        public Lane(int lane, int distance, Car car, List<Trouble> troubles)
        {
            LaneNumber = lane;
            Distance = distance;
            Car = car;
            DistanceTravelled = 0;
            SetTroubles(troubles);
        }

        // Sets a unique range for each Trouble, based on their probability
        // Range is used to determine what Trouble happened (if it happened)
        public void SetTroubles(List<Trouble> troubles)
        {
            int startRange = 1;

            foreach(var t in troubles)
            {
                int endRange = startRange + t.Probability -1;
                Troubles.Add(new Range(startRange, endRange), t);
                startRange = endRange +1;
            }
        }

        public async Task<Lane> Driving()
        {
           
            var r = new Random();
            Car.Start();
            Stopwatch.Start();

            while(DistanceTravelled < Distance)
            {
                Console.SetCursorPosition(0, 1);
                DistanceTravelled += KmhConverter(Car.CurrentSpeed);
                await Task.Delay(1000);
                _troubleTimer++;
                if (_troubleTimer == _troubleInterval)
                {
                    _troubleTimer = 0;

                    int troubleSeed = r.Next(1, 51);

                    foreach(var trouble in Troubles)
                    {
                        if(trouble.Key.Start.Value <= troubleSeed && trouble.Key.End.Value >= troubleSeed)
                        {
                            Console.WriteLine($"OH NO {Car.Name}! {trouble.Value.Name} \n " +
                                              $"{trouble.Value.EffectDesc}\n");

                            if(trouble.Value is Trouble_Delay)
                            {
                                await Task.Delay(trouble.Value.TroubleResult());
                            }
                            else if(trouble.Value is Trouble_Functionality)
                            {
                                Car.CurrentSpeed += trouble.Value.TroubleResult();
                            }
                            break;
                        }
                    }

                }
            }
            Stopwatch.Stop();
            
            return this;
        }


        public int KmhConverter(int kmh)
        {
            double Mps = kmh / 3.6;

            return (int)Mps;
        }


    }
}
