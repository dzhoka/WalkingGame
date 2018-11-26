using NUnit.Framework;
using System.Collections.Generic;
namespace WalkingGame
{
    [TestFixture]
    public class GameTests
    {
        public class StabRandom : IRandom
        {
            List<int> numbers;

            public StabRandom(List<int> numbers)
            {
                this.numbers = numbers;
            }

            public int Next(int n)
            {
                int number = numbers[0];
                numbers.RemoveAt(0);
                return number;
            }
        }

        [Test]
        public void MoveObstacleRowsWhenObstacleAtTheEnd()
        {
            IRandom random = new StabRandom(new List<int> { 0, 0 });
            Game game = new Game(10, random);

            List<char> topRow = game.GetTopRow().GetRow();
            topRow[9] = 'V';
            List<char> bottomRow = game.GetBottomRow().GetRow();

            game.MoveObstacleRows();

            Assert.True(topRow.Count == 10);
            Assert.True(bottomRow.Count == 10);
            Assert.True(topRow[9] == ' ');
            Assert.True(bottomRow[9] == ' ');
        }

        [Test]
        public void MoveObstacleRowsWhenAddTopObstacleRow()
        {
            IRandom random = new StabRandom(new List<int> { 0, 1 });
            Game game = new Game(10, random);

            game.MoveObstacleRows();
            List<char> topRow = game.GetTopRow().GetRow();
            List<char> bottomRow = game.GetBottomRow().GetRow();

            Assert.True(topRow[9] == 'V');
            Assert.True(bottomRow[9] == ' ');
        }

        [Test]
        public void MoveObstacleRowsWhenAddBottomObstacleRow()
        {
            IRandom random = new StabRandom(new List<int> { 1, 1 });
            Game game = new Game(10, random);

            game.MoveObstacleRows();

            List<char> topRow = game.GetTopRow().GetRow();
            List<char> bottomRow = game.GetBottomRow().GetRow();

            Assert.True(bottomRow[9] == 'A');
            Assert.True(topRow[9] == ' ');
        }
    }
}
