using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lines.GameEngine
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public BubbleSize? Contain { get; set; }
        public BubbleColor? Color { get; set; }
    }
}
