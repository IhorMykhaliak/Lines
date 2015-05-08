using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.PathFindingAlgorithm.Interface
{
    interface IPathFindingStrategy
    {
        bool TryGetPath(Field field, Cell cellFrom, Cell cellTo, out List<Cell> path);
    }
}
