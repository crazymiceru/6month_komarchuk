using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class OnGroundController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("OnFloorController");
        private UnitM _unit;
        private IInteractive _iInteractive;
        private ListControllers _listControllers;
        private int countFloor;

        internal OnGroundController(UnitM unit, IInteractive iInteractive, ListControllers listControllers)
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

            if (countFloor > 0)
            {
                _unit.isOnGround = true;               
            }

            else _unit.isOnGround = false;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}