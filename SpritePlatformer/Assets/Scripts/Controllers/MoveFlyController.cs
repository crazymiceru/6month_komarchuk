using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class MoveFlyController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("MoveFlyController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;
        private DataUnit _unitData;

        internal MoveFlyController(UnitM unit, IUnitView unitView, DataUnit unitData, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _unitData = unitData;
        }

        public void Execute(float deltaTime)
        {
            Move(deltaTime);
            SetLimits();
        }

        private void SetLimits()
        {
            var velocity = _unitView.objectRigidbody2D.velocity;
            velocity = Vector3.ClampMagnitude(velocity, _unitData.maxSpeed);
            _unitView.objectRigidbody2D.velocity = velocity;
        }

        private void Move(float deltaTime)
        {
            _unitView.objectRigidbody2D.AddForce(_unit.control * deltaTime * _unitData.powerMove);
            if (_unitView.objectRigidbody2D.velocity.sqrMagnitude != 0) _unit.command = Commands.fly;
            else _unit.command = Commands.stop;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}