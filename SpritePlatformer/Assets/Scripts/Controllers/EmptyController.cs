using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class EmptyCntroller : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;

        internal EmptyCntroller(UnitM unit, IUnitView unitView, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}