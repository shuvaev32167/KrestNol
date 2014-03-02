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

        private bool ActionExternalFile(string[] input)
        {
            switch (input.First().ToLower())
            {
                case "save":
                    ExternalFile.Save(input.Last(), _game);
                    return true;
                case "load":
                    ExternalFile.Load(input.Last(), ref _game);
                    _game.LoadGame();
                    return true;
                case "exit":
                    Environment.Exit(0);
                    break;
            }
            return false;
        }

        public Input(Game g)
        {
            _game = g;
        }
        public ParseAnswer ParseInput(string input, out int value)
        {
            string[] bufStrings = input.Split(' ');
            if (ActionExternalFile(bufStrings))
            {
                value = 0;
                return ParseAnswer.ActionExternalFile;
            }
            if (bufStrings.Count() == 1)
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
            if (ActionExternalFile(bufStrings))
            {
                value = null;
                return ParseAnswer.ActionExternalFile;
            }
            if (bufStrings.Count() != 2)
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

        public ParseAnswer ParseInput(string input, out string value)
        {
            string[] bufStrings = input.Split(' ');
            if (ActionExternalFile(bufStrings))
            {
                value = "";
                return ParseAnswer.ActionExternalFile;
            }
            value = input;
            return ParseAnswer.Ok; 
        }
    }
}
