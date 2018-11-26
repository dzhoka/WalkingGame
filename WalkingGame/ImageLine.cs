using System;
using System.Collections.Generic;
using System.Text;
namespace WalkingGame
{
    public class ImageLine
    {
        protected List<char> row = new List<char>();
        protected const int characterPlaceOffset = 2;

        public ImageLine(int rowLength)
        {
            for (int i = 0; i < rowLength; i++)
            {
                row.Add(' ');
            }
        }

        public List<char> GetRow() { return row; }

        public string BuildImageLine(string bodyPart)
        {
            string imageLine = string.Join("", row.ToArray());
            StringBuilder builder = new StringBuilder(imageLine);

            for (int i = 0; i < bodyPart.Length; i++)
            {
                builder[i + characterPlaceOffset] = bodyPart[i];
            }

            return builder.ToString();
        }

        public void PrintLine()
        {
            foreach (var r in row)
            {
                Console.Write(r);
            }
            Console.WriteLine();
        }

    }
}
