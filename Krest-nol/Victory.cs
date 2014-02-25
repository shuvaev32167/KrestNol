using System.Linq;

namespace KrestNol
{
    public class Victory
    {
        public Victory(Game g)
        {
            _game = g;
        }
        private readonly Game _game;
        public int ZapolnenostyPole { get; set; }
        //TODO Переименовать Из IsVictory в IsEndOfGame
        public bool IsVictory;
        public bool IsVictoryPlayer;

        public void CalculateVictory(int [] pos)
        {
            ++ZapolnenostyPole;
            
            int povtor = 1;
            for (int i = pos.Last()+1; i < _game.SizePoleX; ++i)
            {
                if (_game.Pole[pos.First()][i] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.Last()-1; i >= 0; --i)
            {
                if (_game.Pole[pos.First()][i] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
            
            povtor = 1;
            for (int i = pos.First()+1; i < _game.SizePoleY; ++i)
            {
                if (_game.Pole[i][pos.Last()] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.First()-1; i >= 0; --i)
            {
                if (_game.Pole[i][pos.Last()] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
            
            povtor = 1;
            for (int i = pos.First()+1, j = pos.Last()+1; i < _game.SizePoleY && j < _game.SizePoleX; ++i, ++j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.First()-1, j = pos.Last()-1; i >= 0 && j >= 0; --i, --j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
            
            povtor = 1;
            for (int i = pos.First()+1, j = pos.Last()-1; i < _game.SizePoleY && j >= 0; ++i, --j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }

            for (int i = pos.First()-1, j = pos.Last()+1; i >= 0 && j < _game.SizePoleX; --i, ++j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.First()][pos.Last()])
                    ++povtor;
                if (povtor < _game.VRyd) continue;
                IsVictory = true;
                IsVictoryPlayer = true;
                return;
            }
           
            if (_game.SizePole != ZapolnenostyPole) return;
            IsVictory = true;
            IsVictoryPlayer = false;
        }
    }
}
