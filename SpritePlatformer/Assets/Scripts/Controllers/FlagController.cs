using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class FlagController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("FlagController");
        private UnitM _unit;
        private ListControllers _listControllers;
        private GameM _gameM;
        private DataUnit _dataUnit;

        internal FlagController(UnitM unit, IInteractive iInteractive, GameM gameM, DataUnit dataUnit, ListControllers listControllers)
        {
            _unit = unit;
            iInteractive.evtAttack += Attack;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _gameM = gameM;
            _dataUnit = dataUnit;
            _gameM.countFlags++;
        }

        private (int, bool) Attack(PackInteractiveData pack)
        {
            if (pack.typeItem == TypeItem.Player && _unit.typeAnimation == TypeAnimation.Idle)
            {
                _unit.typeAnimation = TypeAnimation.JumpUp;
                _gameM.countFlags--;
                return (_dataUnit.addScores, true);
            }
            return (0, false);
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}