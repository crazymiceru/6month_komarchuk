using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class StateWalkingController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("StateController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;

        internal StateWalkingController(UnitM unit, IUnitView unitView, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            if (_unit.isJump && _unit.typeAnimation != TypeAnimation.JumpUp && _unit.isOnFloor)
            {
                _unit.typeAnimation = TypeAnimation.JumpUp;
            }
            if (_unitView.objectRigidbody2D.velocity.y < -0.1f && _unit.typeAnimation == TypeAnimation.JumpUp)
            {
                _unit.typeAnimation = TypeAnimation.JumpDown;
            }

            if (_unit.isOnFloor && _unit.typeAnimation != TypeAnimation.JumpUp)
            {
                if (Mathf.Abs(_unitView.objectRigidbody2D.velocity.x) > 0.1f)
                {
                    _unit.typeAnimation = TypeAnimation.Run;
                }
                else
                {
                    _unit.typeAnimation = TypeAnimation.Idle;
                }
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}