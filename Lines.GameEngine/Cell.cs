using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class Cell
    {
        #region Public Properties

        public int Row { get; set; }
        public int Column { get; set; }
        public BubbleSize? Contain { get; set; }
        public BubbleColor? Color { get; set; }

        #endregion
    }
}
