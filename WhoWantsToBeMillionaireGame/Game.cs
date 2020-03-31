using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsToBeMillionaireGame
{
    public class Game
    {
        public Game()
        {
            this.Prices = new Dictionary<int, int>();

            List<int> pricesList = new List<int>() { 50, 100, 200, 300, 500, 700, 1000, 1500, 2000, 2500, 5000, 10000, 15000, 20000, 100000 };

            for (int i = 1; i <= 15; i++)
            {
                this.Prices.Add(i, pricesList[i - 1]);
            }
            
        }

        public Dictionary<int, int> Prices { get; private set; }
    }
}
