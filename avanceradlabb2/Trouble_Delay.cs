namespace avanceradlabb2
{
    public class Trouble_Delay : Trouble
    {
        public int Delay { get; set; }
        public Trouble_Delay(int delay, string name, int prob, string desc) : base(name, prob, desc)
        {
            Delay = delay;
        }
    }
}
