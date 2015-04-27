using System;
using System.Linq;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.PathFinding_Algorithm
{
    public class Map
    {
        #region Public Properties

        public int Width { get; set; }
        public int Height { get; set; }
        public MapElement[,] Elements { get; set; }

        #endregion

        #region Constructors

        public Map(Field Field)
        {
            this.Height = Field.Height;
            this.Width = Field.Width;

            Elements = new MapElement[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    bool isAvailable = (Field.Cells[i, j].Contain == BubbleSize.Big) ? false : true;
                    Elements[i, j] = new MapElement(i, j, -1, 0, 0, isAvailable);
                }
            }
        }

        #endregion

        #region Methods

        public MapElement[] GetAvailableNeighboors(MapElement current)
        {
            MapElement[] neighboors = new MapElement[4];
            if ((current.Row - 1 >= 0) && (Elements[current.Row - 1, current.Column].IsAvailable))
            {
                neighboors[0] = Elements[current.Row - 1, current.Column];
            }
            if ((current.Row + 1 <= Height - 1) && (Elements[current.Row + 1, current.Column].IsAvailable))
            {
                neighboors[1] = Elements[current.Row + 1, current.Column];
            }
            if ((current.Column - 1 >= 0) && (Elements[current.Row, current.Column - 1].IsAvailable))
            {
                neighboors[2] = Elements[current.Row, current.Column - 1];
            }
            if ((current.Column + 1 <= Width - 1) && (Elements[current.Row, current.Column + 1].IsAvailable))
            {
                neighboors[3] = Elements[current.Row, current.Column + 1];
            }

            neighboors = neighboors.Where(x => x != null).ToArray();

            return neighboors;
        }

        public MapElement GetElementById(int id)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Elements[i, j].Id == id)
                    {
                        return Elements[i, j];
                    }
                }
            }
            throw new InvalidOperationException("This id dont exsist!");
        }

        #endregion
    }
}
