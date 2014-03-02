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
        public bool IsEndOfGame { get; private set; }
        public bool IsVictoryPlayer { get; private set; }

        public void CalculateVictory(Point pos)
        {
            ++ZapolnenostyPole;
            
            int povtor = 1;
            for (int i = pos.X+1; i < _game.SizePole; ++i)
            {
                if (_game.Pole[pos.Y][i] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.X-1; i >= 0; --i)
            {
                if (_game.Pole[pos.Y][i] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            
            povtor = 1;
            for (int i = pos.Y+1; i < _game.SizePole; ++i)
            {
                if (_game.Pole[i][pos.X] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.Y-1; i >= 0; --i)
            {
                if (_game.Pole[i][pos.X] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            
            povtor = 1;
            for (int i = pos.Y+1, j = pos.X+1; i < _game.SizePole && j < _game.SizePole; ++i, ++j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.Y-1, j = pos.X-1; i >= 0 && j >= 0; --i, --j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            
            povtor = 1;
            for (int i = pos.Y+1, j = pos.X-1; i < _game.SizePole && j >= 0; ++i, --j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }
            for (int i = pos.Y-1, j = pos.X+1; i >= 0 && j < _game.SizePole; --i, ++j)
            {
                if (_game.Pole[i][j] == _game.Pole[pos.Y][pos.X])
                    ++povtor;
                if (povtor < _game.WinSequenceLength) continue;
                IsEndOfGame = true;
                IsVictoryPlayer = true;
                return;
            }

            if (_game.SizePole * _game.SizePole != ZapolnenostyPole) return;
            IsEndOfGame = true;
            IsVictoryPlayer = false;
        }
    }
}
