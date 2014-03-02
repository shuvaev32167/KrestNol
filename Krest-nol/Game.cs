using System;
using System.Globalization;
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

        public Game()
        {
            _input = new Input(this);
            _victory = new Victory(this);
        }

        [JsonProperty]
        public int PlayerCount { get; set; }

        private int _currentPlayer;

        [JsonProperty]
        private int CurrentPlayer
        {
            get { return _currentPlayer - 48; }
            set
            {
                int bufValue = value;
                if (bufValue >= PlayerCount)
                    bufValue = 0;
                if (bufValue < 0)
                    bufValue = PlayerCount - 1;
                _currentPlayer = bufValue + 48;
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
            SizePole = _input.ConvertInt(Console.ReadLine());
            while (SizePole < 2 || SizePole > 10)
            {
                Console.WriteLine("Не верные размеры поля");
                SizePole = _input.ConvertInt(Console.ReadLine());
            }
            Console.Write("Введите число повторений в ряду: ");
            WinSequenceLength = _input.ConvertInt(Console.ReadLine());
            while (WinSequenceLength < 2 || WinSequenceLength > SizePole)
            {
                Console.WriteLine("Не верное число повторений");
                WinSequenceLength = _input.ConvertInt(Console.ReadLine());
            }
            Console.Write("Введите число игроков: ");
            PlayerCount = _input.ConvertInt(Console.ReadLine());
            while (PlayerCount < 1 || PlayerCount > (SizePole*SizePole) / WinSequenceLength)
            {
                Console.WriteLine("Не верное число игроков");
                PlayerCount = _input.ConvertInt(Console.ReadLine());
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
            StartGame();
        }

        public void StartGame()
        {
            do
            {
                DisplayPole();
                Console.WriteLine("Ходит игрок " + CurrentPlayer.ToString(CultureInfo.InvariantCulture));
                Point pos = _input.ConvertArrInt(Console.ReadLine());
                while (pos == null || pos.Y < 0 || pos.Y >= SizePole || pos.X < 0 || pos.X >= SizePole)
                {
                    if (pos != null)
                        Console.WriteLine("Не верные координаты");
                    pos = _input.ConvertArrInt(Console.ReadLine());
                }
                while (Pole[pos.Y][pos.X] != ' ')
                {
                    Console.WriteLine("Ячейка занята");
                    pos = _input.ConvertArrInt(Console.ReadLine());
                }
                Pole[pos.Y][pos.X] = Convert.ToChar(_currentPlayer);
                _victory.CalculateVictory(pos);
                CurrentPlayer++;
            } while (!_victory.IsEndOfGame);
            DisplayPole();
            if (_victory.IsVictoryPlayer)
                Console.WriteLine("Победил игрок № " + (CurrentPlayer - 1).ToString(CultureInfo.InvariantCulture));
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
