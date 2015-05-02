using System;
using System.Collections.Generic;
using System.Linq;

namespace Lines.GameEngine.PathFinding_Algorithm
{
    /*
     * Review GY: рекомендую переіменувати класс, наприклад, на PathHelper
     */
    public class FindPath
    {
        #region Private Fields

        private List<MapElement> _openList = new List<MapElement>();
        private List<MapElement> _closeList = new List<MapElement>();
        private Field _field;
        private int _lineWeight = 10;
        private int _turn = 0;
        private Map _map;
        private MapElement _elementTo;
        private MapElement _elementFrom;

        #endregion

        #region constructor

        /*
         * Review GY: імена параметрів повинні починатись з маленької літери (Field).
         */
        public FindPath(Field Field, Cell cellFrom, Cell cellTo)
        {
            _field = Field;
            _map = new Map(Field);
            _elementFrom = _map.Elements[cellFrom.Row, cellFrom.Column];
            _elementTo = _map.Elements[cellTo.Row, cellTo.Column];
        }

        #endregion

        #region Methods

        /*
         * Review GY: імена параметрів повинні починатись з маленької літери (FieldWay).
         */
        public bool GetWay( out List<Cell> FieldWay)
        {
            var Way = new List<MapElement>();

            if (_elementFrom == _elementTo)
            {
                Way.Add(_elementTo);
                FieldWay = ConvertToField(Way);
                return true;
            }

            MapElement currElement = new MapElement(
                _elementFrom.Row,
                _elementFrom.Column,
                -1,
                0,
                (Math.Abs(_elementFrom.Row - _elementTo.Row) + Math.Abs(_elementFrom.Column - _elementTo.Column)) * _lineWeight,
                true);

            _map.Elements[currElement.Row, currElement.Column] = currElement;
            _openList.Add(currElement);
            while (_openList.Count > 0 && !WayFound())
            {
                _turn++;
                currElement = _openList.Min();
                _map.Elements[currElement.Row, currElement.Column] = currElement;
                Step(currElement);
            }
            if (WayFound())
            {
                Way.Add(_elementTo);
                Way.Add(currElement);
                while (currElement.ParentId >= 0)
                {
                    currElement = _map.GetElementById(currElement.ParentId);
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

        #endregion

        #region Helpers

        private bool WayFound()
        {
            return (_openList.Find(x => x.Id == _elementTo.Id) != null) ? true : false;
        }

        private void Step(MapElement element)
        {
            foreach (var item in _map.GetAvailableNeighboors(element))
            {
                var temp = new MapElement(
                    item.Row,
                    item.Column,
                    element.Id,
                    element.G + _lineWeight,
                    (Math.Abs(item.Row - _elementTo.Row) + Math.Abs(item.Column - _elementTo.Column)) * _lineWeight,
                     true);
                MapElement dublicateCloseList = _closeList.Find(x => x.Id == temp.Id);
                MapElement dublicateOpenList = _openList.Find(x => x.Id == temp.Id);
                if (dublicateCloseList == null)
                {
                    if (dublicateOpenList == null)
                    {
                        _openList.Add(temp);
                        _map.Elements[temp.Row, temp.Column] = temp;

                    }
                    else
                    {
                        if (dublicateOpenList.F >= temp.F)
                        {
                            _openList.Remove(dublicateOpenList);
                            _openList.Add(item);
                            _map.Elements[temp.Row, temp.Column] = temp;
                        }
                    }
                }
            }
            _closeList.Add(element);
            _openList.Remove(element);
        }

        /*
         * Review GY: імена параметрів повинні починатись з маленької літери (MapWay).
         */
        private List<Cell> ConvertToField(List<MapElement> MapWay)
        {
            List<Cell> FieldWay = new List<Cell>();
            foreach (var item in MapWay)
            {
                FieldWay.Add(_field.Cells[item.Row, item.Column]);
            }

            return FieldWay;
        }

        #endregion
    }
}
