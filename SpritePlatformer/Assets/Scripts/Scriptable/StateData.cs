using System;

namespace SpritePlatformer
{
    [Serializable]
    public sealed class StateData
    {
        public TypeAnimation CurrentAnimation;
        public Commands command;
        public TypeAnimation TargetAnimation;
    }
}
