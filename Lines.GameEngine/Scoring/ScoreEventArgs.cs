using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.Scoring
{
    public class ScoreEventArgs : EventArgs 
    {
        public int Points { get; private set; }
        public ScoreEventArgs(int points)
        {
            Points = points;
        }
    }
}
