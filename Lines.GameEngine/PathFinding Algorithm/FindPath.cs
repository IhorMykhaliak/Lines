using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.PathFinding_Algorithm
{
    public static class FindPath
    {
        private static List<MapElement> _openList = new List<MapElement>();
        private static List<MapElement> _closeList = new List<MapElement>();
        private static NetOfCells _field;
        private static int _lineWeight = 10;
        private static int _turn = 0;

        public static Map Map { get; set; }
        public static MapElement ElementTo { get; set; }
        public static MapElement ElementFrom { get; set; }

        public static bool GetWay(NetOfCells Field, Cell cellFrom, Cell cellTo, out List<Cell> FieldWay)
        {
            #region Validation
            //if (Field == null)
            //{
            //    throw new ArgumentNullException("Field wasn't initialized");
            //}
            //if (cellFrom.Row < 0 && cellFrom.Column < 0 && cellFrom.Row >= Field.Width && cellFrom.Column >= Field.Height)
            //{
            //    throw new InvalidOperationException("Impossible starting cell");
            //}
            //if (cellTo.Row < 0 && cellTo.Column < 0 && cellTo.Row >= Field.Width && cellTo.Column >= Field.Height)
            //{
            //    throw new InvalidOperationException("Impossible final cell");
            //}
            #endregion

            _field = Field;
            Map = new Map(Field);
            ElementFrom = Map.Elements[cellFrom.Row, cellFrom.Column];
            ElementTo = Map.Elements[cellTo.Row, cellTo.Column];

            var Way = new List<MapElement>();

            if (ElementFrom == ElementTo)
            {
                Way.Add(ElementTo);
                FieldWay = ConvertToField(Way);
                return true;
            }

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
                Way.Add(ElementTo);
                Way.Add(currElement);
                while (currElement.ParentId >= 0)
                {
                    currElement = Map.GetElementById(currElement.ParentId);
                    Way.Add(currElement);
                }
                Way.Reverse();
                FieldWay = ConvertToField(Way);
                _openList.Clear();
                _closeList.Clear();
                return true;
            }
            else
            {
                FieldWay = null;
                _openList.Clear();
                _closeList.Clear();
                return false;
            }
        }

        private static bool WayFound()
        {
            return (_openList.Find(x => x.Id == ElementTo.Id) != null) ? true : false;
        }

        private static void Step(MapElement element)
        {
            foreach (var item in Map.GetAvailableNeighboors(element))
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
                        if (dublicateOpenList.F >= temp.F)
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

        private static List<Cell> ConvertToField(List<MapElement> MapWay)
        {
            List<Cell> FieldWay = new List<Cell>();
            foreach (var item in MapWay)
            {
                FieldWay.Add(_field.Cells[item.Row, item.Column]);
            }

            return FieldWay;
        }
    }
}
