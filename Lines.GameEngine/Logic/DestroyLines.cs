using System;

namespace Lines.GameEngine.Logic
{
    public class DestroyLines
    {
        #region Constructors

        public DestroyLines(Field field)
        {
            Field = field;
        }

        #endregion

        #region Public Properties

        public Field Field { get; set; }

        #endregion

        #region Methods

        public void DestroyLine(Cell[][] lines)
        {
            foreach (Cell[] line in lines)
            {
                foreach (var cell  in line)
                {
                    cell.Contain = null;
                    cell.Color = null;
                }
            }
        }

        #endregion
    }
}
