using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class MoveWalkController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("MoveController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;
        private Vector2 leftRight;
        private DataUnit _unitData;

        internal MoveWalkController(UnitM unit, IUnitView unitView, DataUnit unitData, ListControllers listControllers)
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
            velocity.y = 0;
            velocity = Vector3.ClampMagnitude(velocity, _unitData.maxSpeed);
            velocity.y = _unitView.objectRigidbody2D.velocity.y;
            _unitView.objectRigidbody2D.velocity = velocity;
        }

        private void Move(float deltaTime)
        {
            leftRight.x = deltaTime * _unitData.powerMove * _unitView.objectRigidbody2D.mass * _unit.control.x;
            _unitView.objectRigidbody2D.AddForce(leftRight);
            if (leftRight.x != 0) _unit.command = Commands.run;
            else _unit.command = Commands.stop;
            if (_unitView.objectRigidbody2D.velocity.y < -0.1f && !_unit.isOnGround) _unit.command = Commands.fall;
            if (_unit.isOnGround) _unit.command = Commands.onGround;
            if (_unit.isJump && _unit.isOnGround) _unitView.objectRigidbody2D.AddForce(_unitData.powerJump * _unitView.objectRigidbody2D.mass * Vector2.up);
            if (_unit.isJump) _unit.command = Commands.jump;

        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}