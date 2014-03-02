namespace KrestNol
{
    class Player
    {
        public string NamePlayer { get; set; }
        public char SymbolPlayer { get; set; }
        public void MakeAMove(Point point, char[][] pole)
        {
            pole[point.Y][point.X] = SymbolPlayer; 
        }
    }
}
