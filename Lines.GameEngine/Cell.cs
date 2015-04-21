using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class Cell
    {
        #region Private Field

        private BubbleSize? _contain;
        private BubbleColor? _color;

        #endregion

        #region Public Properties

        public int Row { get; set; }
        public int Column { get; set; }
        public BubbleSize? Contain
        {
            get
            {
                return _contain;
            }
            set
            {
                if (value == BubbleSize.Big && _contain == BubbleSize.Big)
                {
                    throw new InvalidOperationException("Can't change size from Big to Big!");
                }
                else
                {
                    _contain = value;
                }
            }
        }
        public BubbleColor? Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (this.Contain != null)
                {
                    _color = value;
                }
            }
        }

        #endregion
    }
}
