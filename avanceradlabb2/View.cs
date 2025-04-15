namespace avanceradlabb2
{
    public class View
    {
        string[][][] Screen = new string[][][] { };

        string[][] Lane = new string[][]
        {
            ["=", "=", "=", "=", "="],
            [" ", " ", " ", " ", " "],
            [" ", " ", " ", " ", " "],
            [" ", " ", " ", " ", " "],
            ["=", "=", "=", "=", "="],
        };

        public string[] scenery = { "X", "O", "8" };


        public View(int noOfLanes) 
        {
        
           for(int i = 0; i < noOfLanes; i++)
            {
                Screen = Screen.Append(Lane).ToArray();
            }
            
        }

        

        public void Print() 
        {
            int scenerypos = 4;
            int lastpos = 0;
            for(int i = 0; i < 100; i++)
            {
                if(scenerypos == -1)
                {
                    scenerypos = 4;

                }

                Console.WriteLine(i);
                Console.WriteLine(scenerypos);
                
                Screen[0][1][scenerypos] = "X";
                Console.WriteLine(Screen[0][1][scenerypos]);
                if(scenerypos != 4)
                {
                
                lastpos = scenerypos +1;

                }
               

                    Screen[0][1][lastpos] = " ";
                
                foreach (var arrarr in Screen) 
                {
                    foreach(var arr in arrarr)
                    {
                        foreach(var s in arr)
                        {
                           
                            Console.Write(s);
                        }
                        Console.WriteLine();
                    }           
                }
                scenerypos--;
                Thread.Sleep(200);
                if (scenerypos == 0)
                {
                    Screen[0][1][0] = " ";
                }
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
