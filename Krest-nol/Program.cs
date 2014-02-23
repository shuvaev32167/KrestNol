using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.NewGame();
            Process.Start("pause");
        }
    }
}
