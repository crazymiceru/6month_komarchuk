using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class GameM
    {
        private ControlLeak _controlLeak = new ControlLeak("GameMData");

        internal event Action evtFlags = delegate { };

        public int countFlags
        {
            get => _flags;
            set
            {
                _flags = value;
                evtFlags.Invoke();
            }
        }
        private int _flags;

        public bool isCongratulations;

        public Dictionary<IQuestView,int> questItemsInteractive=new Dictionary<IQuestView, int>();
        public IQuestView takedItem;
        public GameObject goTakedItem;
        public bool isTakedItem;
    }

}