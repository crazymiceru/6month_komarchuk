using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class MoveTrackController : IController, IExecute, IInitialization
    {
        UnitM _unit;
        ITraectory _iTraectory;
        private int _numTraectory;
        private ControlLeak _controlLeak = new ControlLeak("MoveTrack");        
        private ListControllers _listControllers;
        private IUnitView _iUnitView;
        private DataUnit _dataUnit;

        internal MoveTrackController(UnitM unit, ITraectory iTraectory,IUnitView iUnitView, DataUnit dataUnit, ListControllers listControllers)
        {
            _unit = unit;
            _iTraectory = iTraectory;
            _unit.evtKill += Kill;
            _listControllers = listControllers;
            _iUnitView = iUnitView;
            _dataUnit = dataUnit;
        }

        public void Initialization()
        {
            _numTraectory = 0;
        }

        public void Execute(float deltaTime)
        {
            if (_iTraectory.Track.Length > 0)
            {
                _unit.control.x = Mathf.Sign(_iTraectory.Track[_numTraectory].transform.position.x - _iUnitView.objectTransform.position.x) * _iTraectory.Track[_numTraectory].powerMove;
                _unit.control.y = Mathf.Sign(_iTraectory.Track[_numTraectory].transform.position.y - _iUnitView.objectTransform.position.y) * _iTraectory.Track[_numTraectory].powerMove;
                if (Mathf.Abs(_iTraectory.Track[_numTraectory].transform.position.x - _iUnitView.objectTransform.position.x)
                    <= _iTraectory.Track[_numTraectory].powerMove * deltaTime) _unit.control.x = 0;
                if (Mathf.Abs(_iTraectory.Track[_numTraectory].transform.position.y - _iUnitView.objectTransform.position.y)
                    <= _iTraectory.Track[_numTraectory].powerMove * deltaTime) _unit.control.y = 0;

                var d = Utils.SqrDist(_iUnitView.objectTransform.position.Change(z:0), _iTraectory.Track[_numTraectory].transform.position.Change(z: 0));
                if (d < _dataUnit.minSqrLenghthTraectory)
                {
                    _numTraectory++;
                    if (_numTraectory == _iTraectory.Track.Length) _numTraectory = 0;
                    //Debug.Log($"Next Traectory {_unitView.Track[_numTraectory].transform.gameObject.name}");
                }
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}