using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.PathFinding_Algorithm
{
    class FindPath
    {
        private List<MapElement> _openList = new List<MapElement>();
        private List<MapElement> _closeList = new List<MapElement>();
        private int _lineWeight = 10;
        private int _turn = 0;

        public Map Map { get; set; }
        public MapElement ElementTo { get; set; }
        public MapElement ElementFrom { get; set; }

        public FindPath(NetOfCells Field, Cell cellFrom, Cell cellTo)
        {
            Map = new Map(Field);
            ElementFrom = Map.Elements[cellFrom.Row, cellFrom.Column];
            ElementTo = Map.Elements[cellTo.Row, cellTo.Column];
        }

        public bool GetWay(out List<MapElement> Way)
        {
            Way = new List<MapElement>();
            MapElement currElement = new MapElement(
                ElementFrom.Row,
                ElementFrom.Column,
                -1,
                0,
                (Math.Abs(ElementFrom.Row - ElementTo.Row) + Math.Abs(ElementFrom.Column - ElementTo.Column)) * _lineWeight,
                true);
            Map.Elements[currElement.Row, currElement.Column] = currElement;
            _openList.Add(currElement);
            while (_openList.Count > 0 && !WayFound())
            {
                _turn++;
                currElement = _openList.Min();
                Map.Elements[currElement.Row, currElement.Column] = currElement;
                Step(currElement);
            }
            if (WayFound())
            {
                Way.Add(currElement);
                while (currElement.ParentId >= 0)
                {
                    currElement = Map.GetElementById(currElement.ParentId);
                    Way.Add(currElement);
                }
                return true;
            }
            else
            {
                Way = null;
                return false;
            }
        }

        private bool WayFound()
        {
            return (_openList.Find(x => x.Id == ElementTo.Id) != null) ? true : false;
        }

        private void Step(MapElement element)
        {
            if (element == null)
            {
                return;
            }
            foreach (var item in Map.GetNeighboors(element))
            {
                var temp = new MapElement(
                    item.Row,
                    item.Column,
                    element.Id,
                    element.G + _lineWeight,
                    (Math.Abs(item.Row - ElementTo.Row) + Math.Abs(item.Column - ElementTo.Column)) * _lineWeight,
                     true);
                MapElement dublicateCloseList = _closeList.Find(x => x.Id == temp.Id);
                MapElement dublicateOpenList = _openList.Find(x => x.Id == temp.Id);
                if (dublicateCloseList == null)
                {
                    if (dublicateOpenList == null)
                    {
                        _openList.Add(temp);
                        Map.Elements[temp.Row, temp.Column] = temp;

                    }
                    else
                    {
                        if (dublicateOpenList.F > temp.F)
                        {
                            _openList.Remove(dublicateOpenList);
                            _openList.Add(item);
                            Map.Elements[temp.Row, temp.Column] = temp;
                        }
                    }
                }
            }
            _closeList.Add(element);
            _openList.Remove(element);
        }
    }
}
