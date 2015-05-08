using System;
using System.Collections.Generic;
using System.Linq;
using Lines.GameEngine.PathFindingAlgorithm.Interface;

namespace Lines.GameEngine.PathFindingAlgorithm.AStar
{
    public class FindPath : IPathFindingStrategy
    {
        #region Private Fields

        private const int LineWeight = 10;

        private List<MapElement> _openList = new List<MapElement>();
        private List<MapElement> _closeList = new List<MapElement>();
        private Field _field;
        private Map _map;
        private MapElement _elementTo;
        private MapElement _elementFrom;

        #endregion

        #region Methods

        public bool TryGetPath(Field field, Cell cellFrom, Cell cellTo, out List<Cell> path)
        {
            _field = field;
            _map = new Map(field);
            _elementFrom = _map[cellFrom.Row, cellFrom.Column];
            _elementTo = _map[cellTo.Row, cellTo.Column];

            var mapPath = new List<MapElement>();

            if (GetMapPath(out mapPath))
            {
                path = ConvertToFieldPath(mapPath);
                return true;
            }
            path = null;
            return false;
        }

        #endregion

        #region Helpers

        private bool GetMapPath(out List<MapElement> mapPath)
        {
            mapPath = new List<MapElement>();

            if (_elementFrom == _elementTo)
            {
                mapPath.Add(_elementTo);
                return true;
            }

            MapElement currElement = new MapElement(
                _elementFrom.Row,
                _elementFrom.Column,
                -1,
                0,
                CalculateHeurestic(_elementFrom, _elementTo),
                true);

            _map[currElement.Row, currElement.Column] = currElement;
            _openList.Add(currElement);
            while (_openList.Count > 0 && !IsPathFound())
            {
                currElement = _openList.Min();
                _map[currElement.Row, currElement.Column] = currElement;
                Step(currElement);
            }

            bool isPathFound = IsPathFound();

            mapPath = (isPathFound) ? GetPath(currElement) : null;

            _openList.Clear();
            _closeList.Clear();

            return isPathFound;
        }

        private bool IsPathFound()
        {
            return (_openList.Find(x => x.Id == _elementTo.Id) != null);
        }

        private void Step(MapElement element)
        {
            foreach (var item in _map.GetAvailableNeighboors(element))
            {
                var temp = new MapElement(
                    item.Row,
                    item.Column,
                    element.Id,
                    element.G + LineWeight,
                    CalculateHeurestic(item, _elementTo),
                     true);
                MapElement dublicateInCloseList = _closeList.Find(x => x.Id == temp.Id);
                MapElement dublicateInOpenList = _openList.Find(x => x.Id == temp.Id);
                if (dublicateInCloseList == null)
                {
                    if (dublicateInOpenList == null)
                    {
                        _openList.Add(temp);
                        _map[temp.Row, temp.Column] = temp;
                    }
                    else
                    {
                        if (dublicateInOpenList.F >= temp.F)
                        {
                            _openList.Remove(dublicateInOpenList);
                            _openList.Add(item);
                            _map[temp.Row, temp.Column] = temp;
                        }
                    }
                }
            }
            _closeList.Add(element);
            _openList.Remove(element);
        }

        private int CalculateHeurestic(MapElement elementFrom, MapElement elementTo)
        {
            return (Math.Abs(elementFrom.Row - elementTo.Row) + Math.Abs(elementFrom.Column - elementTo.Column)) * LineWeight;
        }

        private List<MapElement> GetPath(MapElement currElement)
        {
            var mapPath = new List<MapElement>();
            mapPath.Add(_elementTo);
            mapPath.Add(currElement);
            while (currElement.ParentId >= 0)
            {
                currElement = _map.GetElementById(currElement.ParentId);
                mapPath.Add(currElement);
            }
            mapPath.Reverse();

            return mapPath;
        }

        private List<Cell> ConvertToFieldPath(List<MapElement> mapPath)
        {
            List<Cell> FieldPath = new List<Cell>();
            foreach (var item in mapPath)
            {
                FieldPath.Add(_field[item.Row, item.Column]);
            }

            return FieldPath;
        }

        #endregion
    }
}
