using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    public class Victory
    {
        public Victory(Game g)
        {
            game = g;
        }
        private Game game;
        public int ZapolnenostyPole { get; set; }
        public bool IsVictory;
        public bool IsVictoryPlayer;

        public bool CalculateVictory(int [] pos)
        {
            return false;
        }
    }
}
