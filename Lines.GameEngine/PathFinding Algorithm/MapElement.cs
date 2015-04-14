using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.PathFinding_Algorithm
{
    public class MapElement : IComparable<MapElement>
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; set; }
        public bool IsAvailable { get; set; }

        public MapElement(int row, int col, int parendId, int g, int h, bool isAvailable)
        {
            this.Row = row;
            this.Column = col;
            this.Id = 10 * row + col;
            this.ParentId = parendId;
            this.G = g;
            this.H = h;
            this.F = g + h;
            this.IsAvailable = isAvailable;
        }

        int IComparable<MapElement>.CompareTo(MapElement other)
        {
            if (other.F > this.F)
                return -1;
            else if (other.F == this.F)
                return 0;
            else
                return 1;
        }
    }
}
