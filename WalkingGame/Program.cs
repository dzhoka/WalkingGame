namespace WalkingGame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IRandom random = new GameRandom();
            Game game = new Game(30, random);
            game.Play();
        }
    }
}
