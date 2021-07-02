using System.Linq;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class QuestItemController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("");
        private UnitM _unit;
        private ListControllers _listControllers;
        private IQuestView _iQuestView;
        private QuestItemCfg _questItemCfg;
        private bool[] isCopletedItem;
        private Reference _reference;
        private IUnitView _iUnitView;
        private PoolInstatiate _poolInstatiate;
        private DataUnit _dataUnit;

        internal QuestItemController(UnitM unit, IQuestView iQuestView, DataUnit dataUnit, QuestItemCfg questItemCfg, PoolInstatiate poolInstatiate, ListControllers listControllers, Reference reference)
        {
            _unit = unit;
            _iQuestView = iQuestView;
            _iUnitView = _iQuestView as IUnitView;
            _iQuestView.evtGetQuestItemCfg += GetQuestItemCfg;
            _iQuestView.evtDestroy += Destroy;
            _iQuestView.evtApplyItem += ApplyItem;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _questItemCfg = questItemCfg;
            _reference = reference;
            _poolInstatiate = poolInstatiate;
            _dataUnit = dataUnit;
            isCopletedItem = new bool[questItemCfg.completeItems.Length];
        }

        private QuestItemCfg GetQuestItemCfg()
        {
            return _questItemCfg;
        }

        private (bool isOk, int scores) ApplyItem(int item)
        {
            int i;
            bool isApply = false;
            int scores = 0;

            i = 0;
            while (isApply == false && i < _questItemCfg.completeItems.Length)
            {
                if (!isCopletedItem[i] && _questItemCfg.completeItems[i] == item)
                {
                    isCopletedItem[i] = true;
                    isApply = true;
                }
                i++;
            }

            if (_questItemCfg.completeItems.Length>0 && isCopletedItem.All(i => i))
            {
                Debug.Log($"Элемент  {(_iQuestView as MonoBehaviour).name } закрыт");
                if (_questItemCfg.copletePrefab != null)
                {
                    var go = GameObject.Instantiate(_questItemCfg.copletePrefab, _reference.Trash);
                    Utils.InstantiateCntr(go, _poolInstatiate, _listControllers, _reference);
                    go.SetPositionSpriteGround(_iUnitView.objectTransform.gameObject);
                }
                _unit.HP = -1000;
                scores = _dataUnit.addScores;
            }

            return (isApply, scores);
        }

        private int Destroy()
        {
            _unit.HP = -1000;
            return _dataUnit.addScores;
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}