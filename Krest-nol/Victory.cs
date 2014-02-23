using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    public class Victory
    {
        public Victory(Game g)
        {
            game = g;
        }
        private Game game;
        public int ZapolnenostyPole { get; set; }
        public bool IsVictory;
        public bool IsVictoryPlayer;

        public void CalculateVictory(int [] pos)
        {
            ++ZapolnenostyPole;
            
            int povtor = 1;
            for (int i = pos.Last()+1; i < game.SizePoleX; ++i)
            {
                if (game.Pole[pos.First()][i] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            for (int i = pos.Last()-1; i >= 0; --i)
            {
                if (game.Pole[pos.First()][i] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            
            povtor = 1;
            for (int i = pos.First()+1; i < game.SizePoleY; ++i)
            {
                if (game.Pole[i][pos.Last()] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            for (int i = pos.First()-1; i >= 0; --i)
            {
                if (game.Pole[i][pos.Last()] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            
            povtor = 1;
            for (int i = pos.First()+1, j = pos.Last()+1; i < game.SizePoleY && j < game.SizePoleX; ++i, ++j)
            {
                if (game.Pole[i][j] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            for (int i = pos.First()-1, j = pos.Last()-1; i >= 0 && j >= 0; --i, --j)
            {
                if (game.Pole[i][j] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            
            povtor = 1;
            for (int i = pos.First()+1, j = pos.Last()-1; i < game.SizePoleY && j >= 0; ++i, --j)
            {
                if (game.Pole[i][j] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            for (int i = pos.First()-1, j = pos.Last()+1; i >= 0 && j < game.SizePoleX; --i, ++j)
            {
                if (game.Pole[i][j] == game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor >= game.VRyd)
                {
                    IsVictory = true;
                    IsVictoryPlayer = true;
                    return;
                }
            }
            if (game.SizePole == ZapolnenostyPole)
            {
                IsVictory = true;
                IsVictoryPlayer = false;
            }
        }
    }
}
