using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class InputController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("InputController");
        private UnitM _unit;
        private ListControllers _listControllers;

        internal InputController(UnitM unit, ListControllers listControllers)
        {
            _unit = unit;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            float v, h;

            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
            _unit.isJump = Input.GetButtonDown("Jump");
            _unit.control.x = h;
            _unit.control.y = v;
        }

        private void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}