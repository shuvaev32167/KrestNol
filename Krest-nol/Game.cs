using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KrestNol
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(Game))]
    public class Game : CustomCreationConverter<Game>
    {
        public override Game Create(Type objectType)
        {
            return new Game();
        }
        private readonly Vvod _vvod;

        private readonly Victory _victory;

        public Game()
        {
            _vvod = new Vvod(this);
            _victory = new Victory(this);
        }
        [JsonProperty]
        public int SizePlayer { get; set; }
        private int _countPlayer;

        [JsonProperty]
        private int CountPlayer
        {
            get { return _countPlayer - 48; }
            set
            {
                int bufValue = value;
                if (bufValue >= SizePlayer)
                    bufValue -= SizePlayer;
                if (bufValue < 0)
                    bufValue += SizePlayer;
                _countPlayer = bufValue + 48;
            }
        }

        [JsonProperty]
        public int SizePoleX { get; set; }
        [JsonProperty]
        public int SizePoleY { get; set; }
        [JsonProperty]
        public int SizePole { get; set; }

        [JsonProperty]
        public char[][] Pole { get; set; }

        [JsonProperty]
        public int VRyd { get; set; }

        public void NewGame()
        {
            Console.Write("Введите размер поля: ");
            int[] arSize = _vvod.ConvertArrInt(Console.ReadLine());
            while (arSize.First() < 2 || arSize.First() > 10 || arSize.Last() < 2 || arSize.Last() > 10)
            {
                Console.WriteLine("Не верные размеры поля");
                arSize = _vvod.ConvertArrInt(Console.ReadLine());
            }
            Console.Write("Введите число повторений в ряду: ");
            VRyd = _vvod.ConvertInt(Console.ReadLine());
            while (VRyd < 2 || (VRyd > arSize.First() || VRyd > arSize.Last()))
            {
                Console.WriteLine("Не верное число повторений");
                VRyd = _vvod.ConvertInt(Console.ReadLine());
            }
            Console.Write("Введите число игроков: ");
            SizePlayer = _vvod.ConvertInt(Console.ReadLine());
            while (SizePlayer < 1 || (SizePlayer > arSize.First() * arSize.Last() / VRyd))
            {
                Console.WriteLine("Не верное число игроков");
                SizePlayer = _vvod.ConvertInt(Console.ReadLine());
            }
            if (arSize != null)
            {
                SizePoleX = arSize.First();
                SizePoleY = arSize.Last();
                SizePole = SizePoleX * SizePoleY;
            }
            CountPlayer = 0;
            Pole = new char[SizePoleY][];
            for (int i = 0; i < SizePoleY; ++i)
                Pole[i] = new char[SizePoleX];
            for (int i = 0; i < SizePoleY; ++i)
                for (int j = 0; j < SizePoleX; ++j)
                    Pole[i][j] = ' ';
            StartGame();
        }

        public void LoadGame()
        {
            int zapolPole = 0;
            for (int i = 0; i < SizePoleY; ++i)
                for (int j = 0; j < SizePoleX; ++j)
                    if (Pole[i][j] != ' ')
                        ++zapolPole;
            _victory.ZapolnenostyPole = zapolPole;
            StartGame();
        }

        public void StartGame()
        {
            int[] pos;
            do
            {
                DisplayPole();
                Console.WriteLine("Ходит игрок " + CountPlayer.ToString(CultureInfo.InvariantCulture));
                pos = _vvod.ConvertArrInt(Console.ReadLine());
                while (pos == null || pos.First() < 0 || pos.First() >= SizePoleY || pos.Last() < 0 || pos.Last() >= SizePoleX)
                {
                    if (pos != null)
                        Console.WriteLine("Не верные координаты");
                    pos = _vvod.ConvertArrInt(Console.ReadLine());
                }
                while (Pole[pos.First()][pos.Last()] != ' ')
                {
                    Console.WriteLine("Ячейка занята");
                    pos = _vvod.ConvertArrInt(Console.ReadLine());
                }
                Pole[pos.First()][pos.Last()] = (char)_countPlayer;
                _victory.CalculateVictory(pos);
                CountPlayer++;
            } while (!_victory.IsVictory);
            DisplayPole();
            if (_victory.IsVictoryPlayer)
                Console.WriteLine("Попедил игрок № " + (CountPlayer - 1).ToString(CultureInfo.InvariantCulture));
            else
                Console.WriteLine("Нечья");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void DisplayPole()
        {
            Console.Clear();
            for (int j = 0; j < SizePoleX; ++j)
                Console.Write(" -");
            Console.WriteLine();
            for (int i = 0; i < SizePoleY; ++i)
            {
                Console.Write('|');
                for (int j = 0; j < SizePoleX; ++j)
                    Console.Write(Pole[i][j] + "|");
                Console.WriteLine();
                for (int j = 0; j < SizePoleX; ++j)
                    Console.Write(" -");
                Console.WriteLine();
            }
        }
    }
}
