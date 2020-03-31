using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsToBeMillionaireGame
{
    public class Player
    {  
        public Player(string name)
        {
            this.Name = name;
            this.Hints = new int[3] { 1, 1, 1 };
            this.ProfitCurrent = 0;
            this.ProfitGuaranteed = 0;
        }

        public string Name { get; set; }

        public int[] Hints { get; set; }

        public string[] HintCallFriends { get; set; }

        public int ProfitGuaranteed { get; private set; }

        public int ProfitCurrent { get; private set; }

        public void RaiseProfitCurrent(int stagePrice)
        {
            this.ProfitCurrent = stagePrice;

            if (this.ProfitCurrent == 500)
            {
                this.ProfitGuaranteed = 500;
            }
            else if (this.ProfitCurrent == 2500)
            {
                this.ProfitGuaranteed = 2500;
            }
            else if (this.ProfitCurrent == 100000)
            {
                this.ProfitCurrent = 100000;
            }
        }

        public int PayProfitGuaranteed()
        {
            return this.ProfitGuaranteed;
        }        
    }
}
