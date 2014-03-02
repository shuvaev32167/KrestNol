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

        public ParseAnswer ParseInput(string input, ref Point value)
        {
            string[] bufStrings = input.Split(' ');
            switch (bufStrings.First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    ExternalFile.Save(bufStrings.Last(), _game);
                    value = null;
                    return ParseAnswer.ActionExternalFile;
                case "load":
                case "Load":
                case "LOAD":
                    ExternalFile.Load(input.Split(' ').Last(), ref _game);
                    _game.LoadGame();
                    value = null;
                    return ParseAnswer.ActionExternalFile;
            }
            if (input.Split(' ').Count() != 2)
            {
                value = null;
                return ParseAnswer.ActionExternalFile;
            }
            int[] bufInts = new int[2];
            if (Int32.TryParse(bufStrings.First(), out bufInts[0]))
            {
                Int32.TryParse(bufStrings.Last(), out bufInts[1]);
                value = new Point(bufInts);
                return ParseAnswer.Ok;
            }
            return ParseAnswer.Error;
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
