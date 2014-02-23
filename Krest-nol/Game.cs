using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    public class Game
    {
        public int SizePlayer { get; set; }
        private int countPlayer;

        private int CountPlayer
        {
            get { return countPlayer-48; }
            set
            {
                int bufValue = value;
                if (bufValue >= SizePlayer)
                    bufValue -= SizePlayer;
                countPlayer = bufValue+48;
            }
        }

        public int SizePoleX { get; set; }
        public int SizePoleY { get; set; }
        public int SizePole { get; set; }

        public char[][] Pole { get; set; }

        public void NewGame()
        {
            Console.Write("Введите число игроков: ");
            SizePlayer = Vvod.ConvertInt(Console.ReadLine());
            Console.Write("Введите размер поля: ");
            int[] arSize = Vvod.ConvertArrInt(Console.ReadLine());
            if (arSize != null)
            {
                SizePoleX = arSize.First();
                SizePoleY = arSize.Last();
                SizePole = SizePoleX*SizePoleY;
            }
            CountPlayer = 0;
            Pole = new char[SizePoleY][];
            for (int i =0; i < SizePoleY; ++i)
                Pole[i] = new char[SizePoleX];
            for (int i =0; i < SizePoleY; ++i)
                for (int j = 0; j < SizePoleX; ++j)
                    Pole[i][j] = ' ';
            StartGame();
        }

        public void LoadGame()
        { }

        public void StartGame()
        {
            int[] pos;
            Victory victory = new Victory(this);
            do
            {
                Process.Start("clr");
                for (int i = 0; i < SizePoleY; ++i)
                {
                    for (int j = 0; j < SizePoleX; ++j)
                        Console.Write(Pole[i][j] + "|");
                    Console.WriteLine();
                }
                Console.WriteLine("Ходит " + CountPlayer.ToString() + " игрок");
                pos = Vvod.ConvertArrInt(Console.ReadLine());
                victory.CalculateVictory(pos);
                Pole[pos.First()][pos.Last()]=(char)countPlayer;
            } while (!victory.IsVictory);
            if (victory.IsVictoryPlayer)
                Console.WriteLine("Попедил игрок №"+CountPlayer.ToString());
            else
                Console.WriteLine("Нечья");
        }
    }
}
