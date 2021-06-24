using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class OnGroundController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("OnFloorController");
        private UnitM _unit;
        private ListControllers _listControllers;        
        private Transform _transform;

        internal OnGroundController(UnitM unit, IUnitView iUnitView, ListControllers listControllers)
        {
            _unit = unit;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _transform = iUnitView.objectTransform;

            if (FindDetectCollision("foot", out OnTriggerView onGroundView)) onGroundView.evtUpdate += DetectGround;
            if (FindDetectCollision("front", out OnTriggerView onFrontView)) onFrontView.evtUpdate += DetectFront;
        }


        private bool FindDetectCollision(string name,out OnTriggerView onGroundView)
        {
            bool isOk = true;
            onGroundView = null;

            var go = _transform.Find(name);
            if (go == null)
            {
                isOk = false;
            }
            onGroundView= go.GetComponent<OnTriggerView>();
            if (onGroundView == null)
            {
                isOk = false;
            }
            return isOk;
        }

        private void DetectGround(Collider2D _, bool isEnter)
        {
            _unit.isOnGround = isEnter;
            if (isEnter) _unit.command = Commands.onGround;            
        }
        private void DetectFront(Collider2D _, bool isEnter)
        {
            _unit.isWallFront = isEnter;
        }

        void Kill() => _listControllers.Delete(this);
    }
}