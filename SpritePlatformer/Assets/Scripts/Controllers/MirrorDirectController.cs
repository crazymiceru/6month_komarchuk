using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class MirrorDirectController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("MirrorDirectController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;

        internal MirrorDirectController(UnitM unit, IUnitView unitView, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            if ((_unit.control.x < 0 && _unitView.objectTransform.localScale.x > 0) ||
                (_unit.control.x > 0 && _unitView.objectTransform.localScale.x < 0)
                )
            {
                _unitView.objectTransform.localScale = new Vector3(-_unitView.objectTransform.localScale.x, _unitView.objectTransform.localScale.y, _unitView.objectTransform.localScale.z);
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}