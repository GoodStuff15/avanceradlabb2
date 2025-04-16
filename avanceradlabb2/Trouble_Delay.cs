namespace avanceradlabb2
{
    public class Trouble_Delay : Trouble
    {
        public int Delay { get; set; }
        public Trouble_Delay(int delayMs, string name, int prob, string desc) : base(name, prob, desc)
        {
            Delay = delayMs;
        }

        public int SetDelay()
        {
            return Delay;
        }
    }
}
