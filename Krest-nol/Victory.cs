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

        private bool IsVictory(int y, int x, int startY, int startX, ref int povtor)
        {
            if (_game.Pole[y][x] == _game.Pole[startY][startX])
                ++povtor;
            if (povtor < _game.WinSequenceLength) return false;
            IsEndOfGame = true;
            IsVictoryPlayer = true;
            return true;
        }

        public void CalculateVictory(Point pos)
        {
            ++ZapolnenostyPole;

            //right
            int povtor = 1;
            for (int i = pos.X + 1; i < _game.SizePole; ++i)
            {
                if (!IsVictory(pos.Y, i, pos.Y, pos.X, ref povtor))
                    return;
            }
            //left
            for (int i = pos.X - 1; i >= 0; --i)
            {
                if (!IsVictory(pos.Y, i, pos.Y, pos.X, ref povtor))
                    return;
            }

            //down
            povtor = 1;
            for (int i = pos.Y + 1; i < _game.SizePole; ++i)
            {
                if (!IsVictory(i, pos.X, pos.Y, pos.X, ref povtor))
                    return;
            }
            //up
            for (int i = pos.Y - 1; i >= 0; --i)
            {
                if (!IsVictory(i, pos.X, pos.Y, pos.X, ref povtor))
                    return;
            }

            //down-right
            povtor = 1;
            for (int i = pos.Y + 1, j = pos.X + 1; i < _game.SizePole && j < _game.SizePole; ++i, ++j)
            {
                if (!IsVictory(i, j, pos.Y, pos.X, ref povtor))
                    return;
            }
            //up-left
            for (int i = pos.Y - 1, j = pos.X - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (!IsVictory(i, j, pos.Y, pos.X, ref povtor))
                    return;
            }

            //down-left
            povtor = 1;
            for (int i = pos.Y + 1, j = pos.X - 1; i < _game.SizePole && j >= 0; ++i, --j)
            {
                if (!IsVictory(i, j, pos.Y, pos.X, ref povtor))
                    return;
            }
            //up-right
            for (int i = pos.Y - 1, j = pos.X + 1; i >= 0 && j < _game.SizePole; --i, ++j)
            {
                if (!IsVictory(i, j, pos.Y, pos.X, ref povtor))
                    return;
            }

            if (_game.SizePole * _game.SizePole != ZapolnenostyPole) return;
            IsEndOfGame = true;
            IsVictoryPlayer = false;
        }
    }
}
