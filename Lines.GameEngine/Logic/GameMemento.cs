using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.Logic
{
    public class GameMemento
    {
        public int Score { get; set; }
        public int Turn { get; set; }
        public Field Field { get; set; }

        public GameMemento(int score, int turn, Field field)
        {
            Score = score;
            Turn = turn;
            Field = new Field(field);
        }
    }
}
