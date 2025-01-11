using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсовая
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string GameMod {  get; set; }

        public Player(string name, int score, string gameMod)
        {
            Name = name;
            Score = score;
            GameMod = gameMod;
        }
    }
}
