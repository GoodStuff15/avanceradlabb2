namespace avanceradlabb2
{
    public class Car
    {
        public string Name { get; private set; }

        public int CurrentSpeed { get; set; } = 0;

        
        
        public Car(string name) 
        {
            Name = name;
        }

        public void Start()
        {
            CurrentSpeed = 120;
        }

        public void Stop()
        {
            CurrentSpeed = 0;
        }




    }
}
