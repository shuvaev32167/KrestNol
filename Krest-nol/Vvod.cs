using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    public class Vvod
    {
        private Game _game;

        public Vvod(Game g)
        {
            _game = g;
        }
        public int ConvertInt(string input)
        {
            if (input.Split(' ').Count() == 1)
            {
                int value = 0;
                Int32.TryParse(input, out value);
                return value;
            }
            switch (input.Split(' ').First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    ExternalFile.Save(input.Split(' ').Last(), _game);
                    break;
                case "load":
                case "Load":
                case "LOAD":
                    ExternalFile.Load(input.Split(' ').Last(), ref _game);
                    _game.LoadGame();
                    break;
                default:
                    break;
            }
            return 0;
        }

        public int[] ConvertArrInt(string input)
        {
            if (input.Split(' ').Count() != 2) 
                return null;
            int[] value = new int[2];
            string[] buf = input.Split(' ');
            if (Int32.TryParse(buf.First(), out value[0]))
            {
                Int32.TryParse(buf.Last(), out value[1]);
                return value;
            }
            switch (buf.First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    ExternalFile.Save(buf.Last(), _game);
                    break;
                case "load":
                case "Load":
                case "LOAD":
                    ExternalFile.Load(input.Split(' ').Last(), ref _game);
                    _game.LoadGame();
                    break;
                default:
                    break;
            }
            value[0] = -1;
            value[1] = -1;
            return value;
        }
    }
}
