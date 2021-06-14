using System.Linq;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class StateController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("StateController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;
        StateMachineCfg _stateMachineCfg;

        internal StateController(UnitM unit, IUnitView unitView, StateMachineCfg stateMachineCfg, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _stateMachineCfg = stateMachineCfg;
            _unit.evtSetCommand += GetCommand;
            _unit.evtKill += Kill;
        }

        public void GetCommand(Commands command)
        {
            
            var targetAnimation = _stateMachineCfg.stateData.Where(x => x.command == command && (x.CurrentAnimation == TypeAnimation.Any || x.CurrentAnimation == _unit.typeAnimation)).Select(x => x.TargetAnimation).FirstOrDefault();
            
            if (targetAnimation != TypeAnimation.Any)
            {
                //Debug.Log($"Start:{_unit.typeAnimation} Command:{command} End:{targetAnimation}");
                _unit.typeAnimation = targetAnimation;
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}