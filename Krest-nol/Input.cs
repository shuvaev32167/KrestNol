using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace KrestNol
{
    public class Input
    {
        private Game _game;

        public Input(Game g)
        {
            _game = g;
        }
        public int ConvertInt(string input)
        {
            if (input.Split(' ').Count() == 1)
            {
                int value;
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
            }
            return null;
        }
    }
}
