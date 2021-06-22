using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class BoxController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("BoxController");
        private UnitM _unit;
        private ListControllers _listControllers;
        private DataUnit _dataUnit;
        private PoolInstatiate _poolInstatiate;
        private Reference _reference;
        private GameObject _gameObject;

        internal BoxController(UnitM unit, IInteractive iInteractive, GameObject gameObject, DataUnit dataUnit, PoolInstatiate poolInstatiate, ListControllers listControllers, Reference reference)
        {
            _unit = unit;
            iInteractive.evtAttack += Attack;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _dataUnit = dataUnit;
            _poolInstatiate = poolInstatiate;
            _reference = reference;
            _gameObject = gameObject;
        }

        private (int, bool) Attack(PackInteractiveData pack)
        {
            if (pack.typeItem == TypeItem.Player && _unit.typeAnimation == TypeAnimation.Idle)
            {
                _unit.typeAnimation = TypeAnimation.JumpUp;
                GameObject go = (GameObject) new CoinBuild(_poolInstatiate, _listControllers, _reference).
                    CreateGameObject(_reference.Trash).
                    SetPosition(_gameObject.transform.position, Quaternion.identity).
                    AddComponents();

                //var rb = pack.gameObject.GetComponent<Rigidbody2D>();
                //Debug.Log($"{pack.gameObject.name} {pack.gameObject.GetComponent<Rigidbody2D>().velocity}");
                go.GetComponent<Rigidbody2D>().velocity=pack.gameObject.GetComponent<Rigidbody2D>().velocity*2;

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