namespace avanceradlabb2
{
    public abstract class Trouble
    {

        public string Name { get; private set; }
        public int Probability { get; private set; }

        public string EffectDesc { get; private set; }

        public Trouble(string name, int prob, string desc) 
        {
            Name = name;
            Probability = prob;
            EffectDesc = desc;
        }
        
    }
}
