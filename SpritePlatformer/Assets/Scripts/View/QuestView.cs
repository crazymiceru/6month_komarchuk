using System;

namespace SpritePlatformer
{
    public class QuestView:UnitView,IQuestView
    {
        public event Func<QuestItemCfg> evtGetQuestItemCfg;
        public event Func<int, (bool isOk, int scores)> evtApplyItem;
        public event Func<int> evtDestroy;

        public (bool isOk, int scores) applyItem(int item)
        {
            return evtApplyItem(item);
        }

        public int Destroy()
        {
            return evtDestroy.Invoke();
        }

        public QuestItemCfg GetQuestItemCfg()
        {
            return evtGetQuestItemCfg.Invoke();
        }

        

    }
}