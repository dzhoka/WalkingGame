using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
namespace WalkingGame
{
    public class Game
    {
        readonly int rowLength;
        readonly IRandom random;

        readonly ObstacleLine topRow;
        readonly ImageLine middleRow;
        readonly ObstacleLine bottomRow;

        readonly List<ImageLine> imageLines = new List<ImageLine>();

        readonly Character character = new Character();

        int updateTime = 500;

        public Game(int rowLength, IRandom random)
        {
            this.rowLength = rowLength;
            this.random = random;

            topRow = new ObstacleLine(rowLength, 'V', random);
            middleRow = new ImageLine(rowLength);
            bottomRow = new ObstacleLine(rowLength, 'A', random);

            imageLines.Add(topRow);
            imageLines.Add(middleRow);
            imageLines.Add(bottomRow);
        }

        public ObstacleLine GetTopRow() { return topRow; }

        public ObstacleLine GetBottomRow() { return bottomRow; }

        public void Play()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            RepeatWithState(State.jump);
                            break;
                        case ConsoleKey.DownArrow:
                            RepeatWithState(State.squat);
                            break;
                    }
                }
                else if (IsObstacleHit())
                {
                    Console.Clear();
                    Console.WriteLine("GAME OVER!");
                    stopWatch.Stop();
                    Console.WriteLine("Your time (sec): " + stopWatch.Elapsed.Seconds);
                    return;
                }
                else
                {
                    UpdateImage(updateTime);
                    updateTime = updateTime - 2;

                    if (character.GetState() == State.walk1)
                        character.SetState(State.walk2);
                    else
                        character.SetState(State.walk1);
                }
            }
        }

        bool IsObstacleHit()
        {
            string[] characterImage = character.GetPose();
            return topRow.IsObstacleHit(characterImage[0]) || bottomRow.IsObstacleHit(characterImage[2]);
        }

        void RepeatWithState(State state)
        {
            for (int i = 0; i < 3; i++)
            {
                character.SetState(state);
                UpdateImage(100);
            }
        }

        void UpdateImage(int time)
        {
            Console.WriteLine("Press up-arrow button to jump over");
            Console.WriteLine("Press down-arrow button to duck under");
            Console.WriteLine();
            PrintBorder();
            topRow.PrintLine();

            string[] characterImage = character.GetPose();
            List<string> lineImages = MakeImageWithCharacter(characterImage);
            foreach(var line in lineImages){ Console.WriteLine(line); }

            PrintBorder();

            Thread.Sleep(time);
            Console.Clear();

            MoveObstacleRows();
        }

        List<string> MakeImageWithCharacter(string[] characterImage)
        {
            List<string> image = new List<string>();

            for (int i = 0; i < imageLines.Count; i++)
            {
                string line = imageLines[i].BuildImageLine(characterImage[i]);
                image.Add(line);
            }

            return image;
        }

        public void MoveObstacleRows()
        {
            topRow.RemoveFirst();
            bottomRow.RemoveFirst();

            if (topRow.IsObstacleAtTheEnd() || bottomRow.IsObstacleAtTheEnd())
            {
                topRow.AddSpace();
                bottomRow.AddSpace();
                return;
            }

            if (random.Next(2) == 0)
            {
                topRow.AddObstacle();
                bottomRow.AddSpace();
            }
            else
            {
                bottomRow.AddObstacle();
                topRow.AddSpace();
            }
        }

        void PrintBorder()
        {
            for (int i = 0; i < rowLength; i++)
            {
                Console.Write("Z");
            }
            Console.WriteLine();
        }
    }
}
