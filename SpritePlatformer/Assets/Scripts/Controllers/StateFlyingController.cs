using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class StateFlyingController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("StateFlyingController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;

        internal StateFlyingController(UnitM unit, IUnitView unitView, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            if (_unitView.objectRigidbody2D.velocity.x !=0 || _unitView.objectRigidbody2D.velocity.y != 0)
            {
                _unit.typeAnimation = TypeAnimation.Flying;
            }
            else _unit.typeAnimation = TypeAnimation.Idle;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}