using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class DestroyLines
    {
        public Field Field { get; set; }

        public DestroyLines(Field field)
        {
            Field = field;
        }

        public void Destroy(Cell cellFrom, Cell cellTo)
        {
            int cell1_row = cellFrom.Row;
            int cell1_col = cellFrom.Column;

            int cell2_row = cellTo.Row;
            int cell2_col = cellTo.Column;


            if (cell1_col == cell2_col)
            {
                for (int i = cell1_row; i <= cell2_row; i++)
                {
                    Field.Cells[i, cell1_col].Contain = null;
                    Field.Cells[i, cell1_col].Color = null;
                }
                return;
            }

            if (cell1_row == cell2_row)
            {
                for (int j = cell1_col; j <= cell2_col; j++)
                {
                    Field.Cells[cell1_row, j].Contain = null;
                    Field.Cells[cell1_row, j].Color = null;
                }
                return;
            }

            if (cell1_row <= cell2_row && cell1_col <= cell2_col)
            {
                for (int i = 0; i < cell2_row - cell1_row + 1; i++)
                {
                    Field.Cells[cell1_row + i, cell1_col + i].Contain = null;
                    Field.Cells[cell1_row + i, cell1_col + i].Color = null;
                }
                return;
            }

            if (cell1_row <= cell2_row && cell1_col >= cell2_col)
            {
                for (int i = 0; i < cell2_row - cell1_row + 1; i++)
                {
                    Field.Cells[cell1_row + i, cell1_col - i].Contain = null;
                    Field.Cells[cell1_row + i, cell1_col - i].Color = null;
                }
                return;
            }

            throw new InvalidOperationException("Wrong begin/end of the line!");
        }
    }
}
