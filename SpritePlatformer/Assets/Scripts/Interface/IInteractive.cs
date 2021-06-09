using System;
using System.Collections.Generic;

namespace SpritePlatformer
{
    public interface IInteractive
    {
        public int Attack(PackInteractiveData data);
        public event Action evtAnyCollision;
        public event Action<IInteractive, bool> evtCollision;
        public event Func<PackInteractiveData, int> evtAttack;
        public event Action<bool> evtTrigger;
        internal void Kill();
    }
}