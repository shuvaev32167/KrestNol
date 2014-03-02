using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KrestNol
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof (Game))]
    public class Game : CustomCreationConverter<Game>
    {
        public override Game Create(Type objectType)
        {
            return new Game();
        }

        private readonly Input _input;

        private readonly Victory _victory;

        [JsonProperty]
        private Player[] Players { get; set; }

        //[JsonProperty]
        //private Dictionary<int, string> DicPlayers { get; set; }

        public Game()
        {
            _input = new Input(this);
            _victory = new Victory(this);
            //DicPlayers = new Dictionary<int, string>();
        }

        [JsonProperty]
        public int PlayerCount { get; set; }

        private int _currentPlayer;

        [JsonProperty]
        private int CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                int bufValue = value;
                if (bufValue >= PlayerCount)
                    bufValue = 0;
                if (bufValue < 0)
                    bufValue = PlayerCount - 1;
                _currentPlayer = bufValue;
            }
        }

        [JsonProperty]
        public int SizePole { get; private set; }

        [JsonProperty]
        public char[][] Pole { get; private set; }

        [JsonProperty]
        public int WinSequenceLength { get; private set; }

        public void NewGame()
        {
            Console.Write("Введите размер поля: ");
            int sizePole;
            Input.ParseAnswer parceAnsverResult = _input.ParseInput(Console.ReadLine(), out sizePole);
            while (sizePole < 2 || sizePole > 10)
            {
                if (parceAnsverResult == Input.ParseAnswer.Ok || parceAnsverResult == Input.ParseAnswer.Error)
                    Console.WriteLine("Не верные размеры поля");
                else
                {
                    Console.Clear();
                    Console.Write("Введите размер поля: ");
                }
                parceAnsverResult = _input.ParseInput(Console.ReadLine(), out sizePole);
            }
            SizePole = sizePole;
            Console.Clear();
            Console.Write("Введите число повторений в ряду: ");
            int winSequenceLength;
            parceAnsverResult = _input.ParseInput(Console.ReadLine(), out winSequenceLength);
            while (WinSequenceLength < 2 || WinSequenceLength > SizePole)
            {
                if (parceAnsverResult == Input.ParseAnswer.Ok || parceAnsverResult == Input.ParseAnswer.Error)
                    Console.WriteLine("Не верное число повторений");
                else
                {
                    Console.Clear();
                    Console.Write("Введите число повторений в ряду: ");
                }
                parceAnsverResult = _input.ParseInput(Console.ReadLine(), out winSequenceLength);
            }
            Console.Clear();
            WinSequenceLength = winSequenceLength;
            Console.Write("Введите число игроков: ");
            int playerCount;
            parceAnsverResult = _input.ParseInput(Console.ReadLine(), out playerCount);
            while (PlayerCount < 1 || PlayerCount > (SizePole*SizePole) / WinSequenceLength)
            {
                if (parceAnsverResult == Input.ParseAnswer.Ok || parceAnsverResult == Input.ParseAnswer.Error)
                    Console.WriteLine("Не верное число игроков");
                else
                {
                    Console.Clear();
                    Console.Write("Введите число игроков: ");
                }
                parceAnsverResult = _input.ParseInput(Console.ReadLine(), out playerCount);
            }
            Console.Clear();
            PlayerCount = playerCount;
            Players = new Player[PlayerCount];
            for (int i = 0; i < PlayerCount; ++i)
            {
                Console.WriteLine("Введите имя "+i+"-го игрока");
                string buf;
                _input.ParseInput(Console.ReadLine(), out buf);
                Players[i] = new Player
                {
                    NamePlayer = buf,
                    SymbolPlayer = Convert.ToChar(i + 48)
                };
            }
            CurrentPlayer = 0;
            Pole = new char[SizePole][];
            for (int i = 0; i < SizePole; ++i)
                Pole[i] = new char[SizePole];
            for (int i = 0; i < SizePole; ++i)
                for (int j = 0; j < SizePole; ++j)
                    Pole[i][j] = ' ';
            StartGame();
        }

        public void LoadGame()
        {
            int zapolPole = 0;
            for (int i = 0; i < SizePole; ++i)
                for (int j = 0; j < SizePole; ++j)
                    if (Pole[i][j] != ' ')
                        ++zapolPole;
            _victory.ZapolnenostyPole = zapolPole;
            //_players = new Player[PlayerCount];
            StartGame();
        }

        public void StartGame()
        {
            do
            {
                DisplayPole();
                Console.WriteLine("Ходит " + Players[CurrentPlayer].NamePlayer);
                Point pos = null;
                Input.ParseAnswer parceAnsverResult = _input.ParseInput(Console.ReadLine(), ref pos);
                bool isNotCorrectCoordinats = pos == null || pos.Y < 0 || pos.Y >= SizePole || pos.X < 0 ||
                                              pos.X >= SizePole;
                bool isNotCorrectPos = false;
                if (!isNotCorrectCoordinats)
                    isNotCorrectPos = Pole[pos.Y][pos.X] != ' ';
                while (isNotCorrectCoordinats || isNotCorrectPos)
                {
                    if ((parceAnsverResult == Input.ParseAnswer.Ok || parceAnsverResult == Input.ParseAnswer.Error)&&
                        isNotCorrectCoordinats)
                        Console.WriteLine("Не верные координаты");
                    else
                    {
                        if (isNotCorrectPos)
                        {
                            Console.WriteLine("Ячейка занята");
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Ходит " + Players[CurrentPlayer].NamePlayer);
                        }
                    }
                    parceAnsverResult = _input.ParseInput(Console.ReadLine(), ref pos);
                }
                Players[CurrentPlayer].MakeAMove(pos, Pole);
                _victory.CalculateVictory(pos);
                CurrentPlayer++;
            } while (!_victory.IsEndOfGame);
            DisplayPole();
            if (_victory.IsVictoryPlayer)
                Console.WriteLine("Победил игрок № " + (--CurrentPlayer).ToString(CultureInfo.InvariantCulture)+"\n"+
                    "С именем " +Players[CurrentPlayer].NamePlayer);
            else
                Console.WriteLine("Ничья");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void DisplayPole()
        {
            Console.Clear();
            for (int j = 0; j < SizePole; ++j)
                Console.Write(" -");
            Console.WriteLine();
            for (int i = 0; i < SizePole; ++i)
            {
                Console.Write('|');
                for (int j = 0; j < SizePole; ++j)
                    Console.Write(Pole[i][j] + "|");
                Console.WriteLine();
                for (int j = 0; j < SizePole; ++j)
                    Console.Write(" -");
                Console.WriteLine();
            }
        }
    }
}
