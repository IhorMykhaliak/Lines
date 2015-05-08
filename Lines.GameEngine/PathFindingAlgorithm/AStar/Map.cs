using System;
using System.Linq;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.PathFindingAlgorithm.AStar
{
    public class Map
    {
        #region Public Properties

        public int Width { get; set; }
        public int Height { get; set; }
        private MapElement[,] Elements { get; set; }
        public MapElement this[int row, int col]
        {
            get { return Elements[row, col]; }
            set { Elements[row, col] = value; }
        }

        #endregion

        #region Constructors

        public Map(Field field)
        {
            this.Height = field.Height;
            this.Width = field.Width;

            Elements = new MapElement[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    bool isAvailable = (field[i, j].Contain == BubbleSize.Big) ? false : true;
                    this[i, j] = new MapElement(i, j, -1, 0, 0, isAvailable);
                }
            }
        }

        #endregion

        #region Methods

        public MapElement[] GetAvailableNeighboors(MapElement current)
        {
            MapElement[] neighboors = new MapElement[4];
            if ((current.Row - 1 >= 0) && (this[current.Row - 1, current.Column].IsAvailable))
            {
                neighboors[0] = this[current.Row - 1, current.Column];
            }
            if ((current.Row + 1 <= Height - 1) && (this[current.Row + 1, current.Column].IsAvailable))
            {
                neighboors[1] = this[current.Row + 1, current.Column];
            }
            if ((current.Column - 1 >= 0) && (this[current.Row, current.Column - 1].IsAvailable))
            {
                neighboors[2] = this[current.Row, current.Column - 1];
            }
            if ((current.Column + 1 <= Width - 1) && (this[current.Row, current.Column + 1].IsAvailable))
            {
                neighboors[3] = this[current.Row, current.Column + 1];
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
                    if (this[i, j].Id == id)
                    {
                        return this[i, j];
                    }
                }
            }
            throw new InvalidOperationException("This id dont exsist!");
        }

        #endregion
    }
}
