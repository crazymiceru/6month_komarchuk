using System;

namespace SpritePlatformer
{
    internal interface IQuestView
    {
        public event Func<QuestItemCfg> evtGetQuestItemCfg;
        public event Func<int, (bool isOk, int scores)> evtApplyItem;
        public event Func<int> evtDestroy;

        public QuestItemCfg GetQuestItemCfg();
        public (bool isOk, int scores) applyItem(int item);
        public int Destroy();

    }
}