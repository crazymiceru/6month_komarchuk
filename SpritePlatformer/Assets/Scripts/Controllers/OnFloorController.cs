using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class OnFloorController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("OnFloorController");
        private UnitM _unit;
        private IInteractive _iInteractive;
        private ListControllers _listControllers;
        private int countFloor;

        internal OnFloorController(UnitM unit, IInteractive iInteractive, ListControllers listControllers)
        {
            _unit = unit;
            _iInteractive = iInteractive;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _iInteractive.evtTrigger += DetectFloor;
        }

        private void DetectFloor(bool isEnter)
        {
            if (isEnter)
            {
                countFloor++;
            }
            else countFloor--;

            if (countFloor > 0) _unit.isOnFloor = true;
            else _unit.isOnFloor = false;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}