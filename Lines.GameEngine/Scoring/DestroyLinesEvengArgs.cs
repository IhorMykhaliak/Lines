using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.Scoring
{
    public class DestroyLinesEvengArgs
    {
        public Cell[][] Lines { get; private set; }

        public DestroyLinesEvengArgs(Cell[][] lines)
        {
            Lines = lines;
        }
    }
}
