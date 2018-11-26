using System;
namespace WalkingGame
{
    public class GameRandom : IRandom
    {
        readonly Random rnd = new Random();

        public int Next(int n)
        {
            return rnd.Next(n);
        }
    }
}
