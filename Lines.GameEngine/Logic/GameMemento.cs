using System;
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
