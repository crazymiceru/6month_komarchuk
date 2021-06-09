using System;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class MoveController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("MoveController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;
        private Vector2 leftRight;
        private DataUnit _unitData;

        internal MoveController(UnitM unit, IUnitView unitView, DataUnit unitData, ListControllers listControllers)
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
            leftRight.x = _unit.control.x * deltaTime * _unitData.powerMove;
            _unitView.objectRigidbody2D.AddForce(leftRight);
            if (_unit.isJump && _unit.isOnFloor) _unitView.objectRigidbody2D.AddForce(_unitData.powerJump * Vector2.up);
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}