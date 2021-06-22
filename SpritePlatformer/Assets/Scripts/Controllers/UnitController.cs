using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class UnitController : IController, IInitialization
    {
        ControlLeak _controlLeak = new ControlLeak("UnitController");
        private UnitM _unit;
        private IInteractive _iInteractive;
        private DataUnit _unitData;
        private GameObject _gameObject;
        private Reference _reference;
        private ListControllers _listControllers;

        #region Init

        internal UnitController(UnitM unit, IInteractive iInteractive, TypeItem typeItem, DataUnit unitData, ListControllers listControllers, Reference reference)
        {
            _unit = unit;
            _iInteractive = iInteractive;
            _unitData = unitData;
            _reference = reference;
            _listControllers = listControllers;

            _unit.evtKill += _iInteractive.Kill;
            _unit.evtKill += Kill;
            _iInteractive.evtAttack += Attack;
            _iInteractive.evtCollision += OutInteractive;
            _unit.evtDecLives += DecLive;

            _unit.packInteractiveData = new PackInteractiveData(_unitData.AttackPower, typeItem, (iInteractive as MonoBehaviour).gameObject);

            _unit.maxLive = _unitData.maxLive;
            _unit.maxSpeed = _unitData.maxSpeed;

            _gameObject = (_iInteractive as MonoBehaviour).gameObject;

        }

        public void Initialization()
        {
            _unit.HP = _unit.maxLive;
            _unit.scores = 0;
        }

        #endregion


        #region Game

        private void DecLive()
        {
            if (_unitData.destroyEffects != null)
            {
                var go = GameObject.Instantiate(_unitData.destroyEffects, _gameObject.transform.position, Quaternion.identity);
                go.transform.SetParent(_reference.Trash);
                GameObject.Destroy(go, _unitData.timeViewDestroyEffects);
            }
        }


        private (int, bool) Attack(PackInteractiveData pack)
        {
            int addScores = 0;
            bool isDead = false;
            if (_unit.HP != 0)
            {
                _unit.HP -= pack.attackPower;
                if (_unit.HP == 0)
                {
                    //Debug.Log($"Destroy {_gameObject.name} scores:{_unitData.addScores}");
                    addScores = _unitData.addScores;
                    isDead = true;
                }
            }
            return (addScores, isDead);
        }

        private void OutInteractive(IInteractive ui, bool isEnter)
        {
            if (isEnter)
            {
                //Debug.Log($"Object {_gameObject.name} Attack {(ui as MonoBehaviour).name}");
                var scores=ui.Attack(_unit.packInteractiveData).scores;
                _unit.scores += scores;
            }
        }

        private void Kill()
        {
            _listControllers.Delete(this);
        }

        #endregion
    }
}
