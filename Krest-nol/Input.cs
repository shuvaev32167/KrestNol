using System;
using System.Linq;

namespace KrestNol
{
    public class Input
    {
        public enum ParseAnswer
        {
            Ok = 0,
            Error=1,
            ActionExternalFile=2
        }
        private Game _game;

        public Input(Game g)
        {
            _game = g;
        }
        public ParseAnswer ParseInput(string input, out int value)
        {
            switch (input.Split(' ').First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    ExternalFile.Save(input.Split(' ').Last(), _game);
                    value = 0;
                    return ParseAnswer.ActionExternalFile;
                case "load":
                case "Load":
                case "LOAD":
                    ExternalFile.Load(input.Split(' ').Last(), ref _game);
                    _game.LoadGame();
                    value = 0;
                    return ParseAnswer.ActionExternalFile;
            }
            if (input.Split(' ').Count() == 1)
            {
                //int value;
                return Int32.TryParse(input, out value) ? ParseAnswer.Ok : ParseAnswer.Error;
            }
            value = 0;
            return ParseAnswer.Error;   
        }

        public Point ConvertArrInt(string input)
        {
            if (input.Split(' ').Count() != 2) 
                return null;
            int[] value = new int[2];
            string[] buf = input.Split(' ');
            if (Int32.TryParse(buf.First(), out value[0]))
            {
                Int32.TryParse(buf.Last(), out value[1]);
                return new Point(value);
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

        public string ConvertToString(string input)
        {
            switch (input.Split(' ').First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    ExternalFile.Save(input.Split(' ').Last(), _game);
                    return "";
                case "load":
                case "Load":
                case "LOAD":
                    ExternalFile.Load(input.Split(' ').Last(), ref _game);
                    _game.LoadGame();
                    return "";
            }
            return input; 
        }
    }
}
