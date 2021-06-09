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

        internal UnitController(UnitM unit, IInteractive iInteractive, TypeItem typeItem, DataUnit unitData, ListControllers listControllers,Reference reference)
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

            _unit.packInteractiveData = new PackInteractiveData(_unitData.AttackPower, typeItem);
                
            _unit.maxLive = _unitData.maxLive;
            _unit.maxSpeed = _unitData.maxSpeed;

            _gameObject = (_iInteractive as MonoBehaviour).gameObject;

        }

        public void Initialization()
        {
            _unit.HP = _unit.maxLive;
        }

        #endregion


        #region Game

        private void DecLive()
        {
            if (_unitData.destroyEffects != null)
            {
                var go = GameObject.Instantiate(_unitData.destroyEffects, _gameObject.transform.position , Quaternion.identity);
                go.transform.SetParent(_reference.Trash);
                GameObject.Destroy(go, _unitData.timeViewDestroyEffects);
            }
        }

        
        private int Attack(PackInteractiveData pack)
        {
            int addScores = 0;
                if (_unit.HP != 0)
                {
                    _unit.HP -= pack.attackPower;
                    if (_unit.HP == 0) addScores = _unitData.addScores;
                }
            return addScores;
        }

        private void OutInteractive(IInteractive ui, bool isEnter)
        {
            var addScores = ui.Attack(_unit.packInteractiveData);
        }

        private void Kill()
        {
            _listControllers.Delete(this);
        }

        #endregion
    }
}
