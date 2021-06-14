namespace SpritePlatformer
{

    internal sealed class CommandController : IController,IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("CommandController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;

        internal CommandController(UnitM unit, IUnitView unitView, ListControllers listControllers)
        {
            _unit = unit;
            _listControllers = listControllers;
            _unitView = unitView;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            //if (_unit.isOnFloor) _unit.command = Commands.onGround;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}