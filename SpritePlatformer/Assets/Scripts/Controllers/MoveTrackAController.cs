using Pathfinding;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class MoveTrackAController : IController, IInitialization, IExecute
    {
        private UnitM _unit;
        private ITraectory _iTraectory;
        private int _numTraectory;
        private ControlLeak _controlLeak = new ControlLeak("MoveTrackA");
        private ListControllers _listControllers;
        private IUnitView _iUnitView;
        private DataUnit _dataUnit;
        private IAstarAI _astarAI;
        private Seeker _seeker;
        private bool isAtackPlayer;
        private Reference _reference;

        internal MoveTrackAController(UnitM unit, ITraectory iTraectory, IUnitView iUnitView, IAstarAI astarAI, Seeker seeker, DataUnit dataUnit, ListControllers listControllers, Reference reference)
        {
            _unit = unit;
            _iTraectory = iTraectory;
            _unit.evtKill += Kill;
            _listControllers = listControllers;
            _iUnitView = iUnitView;
            _dataUnit = dataUnit;
            _astarAI = astarAI;
            _seeker = seeker;
            _reference = reference;
            if (iTraectory.onTriggerView != null) iTraectory.onTriggerView.evtUpdate += DetectPlayer;
        }

        public void Initialization()
        {
            _numTraectory = 0;
            SetPos();
        }

        public void Execute(float deltaTime)
        {
            if (_iTraectory.Track.Length > 0)
            {
                var d = Utils.SqrDist(_iUnitView.objectTransform.position.Change(z: 0), _iTraectory.Track[_numTraectory].transform.position.Change(z: 0));
                if (d < _dataUnit.minSqrLenghthTraectory)
                {
                    _numTraectory++;
                    if (_numTraectory == _iTraectory.Track.Length) _numTraectory = 0;
                }
            }
            SetPos();
            SetStates();
        }

        private void SetStates()
        {
            if (_astarAI.velocity.sqrMagnitude != 0) _unit.command = Commands.fly;
            else _unit.command = Commands.stop;

            if ((_astarAI.velocity.x < 0 && _iUnitView.objectTransform.localScale.x > 0) ||
            (_astarAI.velocity.x > 0 && _iUnitView.objectTransform.localScale.x < 0)
            )
            {
                _iUnitView.objectTransform.localScale = new Vector3(-_iUnitView.objectTransform.localScale.x, _iUnitView.objectTransform.localScale.y, _iUnitView.objectTransform.localScale.z);
            }
        }

        private void SetPos()
        {
            if (!isAtackPlayer)
            {
                _astarAI.destination = _iTraectory.Track[_numTraectory].transform.position;
                _astarAI.maxSpeed = _iTraectory.Track[_numTraectory].powerMove;
            }
            else
            {
                _astarAI.destination = _reference.player.transform.position;
            }
        }

        private void DetectPlayer(Collider2D _, bool isEnter)
        {
            isAtackPlayer = isEnter;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}