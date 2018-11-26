using System;
using System.Linq;
namespace WalkingGame
{
    public class ObstacleLine : ImageLine
    {
        const int lastElements = 3;

        protected readonly char obstacle;
        readonly IRandom rnd;

        public ObstacleLine(int rowLength, char obstacle, IRandom rnd) : base(rowLength)
        {
            this.obstacle = obstacle;
            this.rnd = rnd;
        }

        public bool IsObstacleAtTheEnd()
        {
            return row.Skip(row.Count - lastElements).Contains(obstacle);
        }

        public bool IsObstacleHit(string bodyPart)
        {
            for (int i = 0; i < bodyPart.Length; i++)
            {
                if (row[i + characterPlaceOffset] == obstacle && bodyPart != "") return true;
            }
            return false;
        }

        public virtual void AddObstacle()
        {
            if (row.Count(r => r == obstacle) > 2)
            {
                AddSpace();
                return;
            }

            if (rnd.Next(2) == 0)
                AddSpace();
            else
                row.Add(obstacle);
        }

        public void RemoveFirst()
        {
            row.RemoveAt(0);
        }

        public void AddSpace()
        {
            row.Add(' ');
        }
    }
}
