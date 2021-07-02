using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SpritePlatformer
{
    internal sealed class QuestInteractionController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("QuestInteractionController");
        private GameM _gameM;
        private ListControllers _listControllers;
        private Image _imageItem;
        private IInteractive _interactive;
        private UnitM _unitM;
        private IUnitView _unitView;

        internal QuestInteractionController(UnitM unitM, GameM gameM, IInteractive interactive, IUnitView unitView, ListControllers listControllers)
        {
            _gameM = gameM;
            _listControllers = listControllers;
            _unitM = unitM;
            _unitM.evtKill += Kill;
            _unitView = unitView;
            var posItem = GameObject.FindObjectOfType<TagPosItem>();
            if (posItem!=null &&posItem.TryGetComponent<Image>(out Image image))
            {
                _imageItem = image;
                _imageItem.enabled = false;
            }
            else Debug.LogWarning($"Dont find SpritePosition (TagPosItem)");

            _interactive = interactive;
            _interactive.evtTrigger += InteractiveItem;
        }

        public void Execute(float deltaTime)
        {
            if (_unitM.isInteractive && _unitM.isOnGround)
            {
                if (!_gameM.isTakedItem && _gameM.questItemsInteractive.Count > 0)
                {
                    var questView = _gameM.questItemsInteractive.Keys.First();
                    var questItemCfg = questView.GetQuestItemCfg();
                    if (questItemCfg.isMove)
                    {
                        _gameM.isTakedItem = true;
                        _gameM.takedItem = questView;
                        _gameM.goTakedItem = (questView as MonoBehaviour).gameObject;
                        _gameM.goTakedItem.SetActive(false);
                        _imageItem.enabled = true;
                        _imageItem.sprite = questItemCfg.image;
                        Debug.Log($"Взяли {_gameM.goTakedItem.name}");
                    }
                }
                else if (_gameM.isTakedItem)
                {
                    if (_gameM.questItemsInteractive.Count == 0)
                    {
                        _gameM.goTakedItem.SetPositionSpriteGround((_unitView as MonoBehaviour).gameObject);
                        _gameM.goTakedItem.SetActive(true);
                        _gameM.isTakedItem = false;
                        Debug.Log($"Положили {_gameM.goTakedItem.name}");
                        _imageItem.enabled = false;
                    }
                    else
                    {
                        var questView = _gameM.questItemsInteractive.Keys.First();
                        var result = questView.applyItem((_gameM.takedItem as IUnitView).GetTypeItem().cfg);
                        if (result.isOk)
                        {
                            _gameM.goTakedItem.SetPositionSpriteGround((_unitView as MonoBehaviour).gameObject);
                            _gameM.goTakedItem.SetActive(true);
                            _gameM.isTakedItem = false;                            
                            _unitM.scores += _gameM.takedItem.Destroy();
                            _unitM.scores += result.scores;
                            Debug.Log($"Провзаимодействовали {_gameM.goTakedItem.name} с {(questView as MonoBehaviour).name}");
                            _imageItem.enabled = false;
                        }
                    }
                }
            }
        }

        private void InteractiveItem(Collider2D collider, bool isEnter)
        {
            if (collider.gameObject.TryGetComponent<IQuestView>(out IQuestView questItem))
            {
                if (isEnter)
                {
                    //Debug.Log($"Вошли в зону {(questItem as MonoBehaviour).name}");
                    if (_gameM.questItemsInteractive.ContainsKey(questItem)) _gameM.questItemsInteractive[questItem]++;
                    else _gameM.questItemsInteractive.Add(questItem, 1);
                }
                else
                {
                    //Debug.Log($"Вышли из {(questItem as MonoBehaviour).name}");
                    if (_gameM.questItemsInteractive.ContainsKey(questItem))
                    {
                        _gameM.questItemsInteractive[questItem]--;
                        if (_gameM.questItemsInteractive[questItem] == 0)
                        {
                            _gameM.questItemsInteractive.Remove(questItem);
                        }
                    }
                }
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}