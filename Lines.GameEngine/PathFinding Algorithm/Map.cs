using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.PathFinding_Algorithm
{
    public class Map
    {
        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }
        public MapElement[,] Elements { get; set; }

        #endregion

        #region Constructor
        
        public Map(NetOfCells Field)
        {
            this.Width = Field.Width;
            this.Height = Field.Height;

            Elements = new MapElement[Width, Height];

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

        public MapElement[] GetAvailableNeighboors(MapElement current)
        {
            MapElement[] neighboors = new MapElement[4];
            if (current.Row - 1 >= 0 && Elements[current.Row - 1, current.Column].IsAvailable)
            {
                neighboors[0] = Elements[current.Row - 1, current.Column];
            }
            if (current.Row + 1 <= Width - 1 && Elements[current.Row + 1, current.Column].IsAvailable)
            {
                neighboors[1] = Elements[current.Row + 1, current.Column];
            }
            if (current.Column - 1 >= 0 && Elements[current.Row, current.Column - 1].IsAvailable)
            {
                neighboors[2] = Elements[current.Row, current.Column - 1];
            }
            if (current.Column + 1 <= Height - 1 && Elements[current.Row, current.Column + 1].IsAvailable)
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
    }
}
