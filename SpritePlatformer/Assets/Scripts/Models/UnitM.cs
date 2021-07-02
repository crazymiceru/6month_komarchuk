using System;
using UnityEngine;

namespace SpritePlatformer
{
    public sealed class UnitM
    {
        private ControlLeak _controlLeak = new ControlLeak("UnitMData");

        internal event Action evtKill = delegate { };
        internal event Action evtLives = delegate { };
        internal event Action evtDecLives = delegate { };
        internal event Action evtScores = delegate { };

        private int _hp = -1;
        internal int maxLive = 0;
        internal float maxSpeed;
        internal PackInteractiveData packInteractiveData;

        internal int HP
        {
            get => _hp;
            set
            {
                if (_hp != value && (_hp > -1 || value > 0 || value==-1000))
                {
                    if (_hp < value)
                    {
                        _hp = value;
                        evtLives.Invoke();
                    }

                    if (_hp > value)
                    {
                            if ((_hp > 0 && value <= 0) || value==-1000)
                            {
                                evtKill();
                            }
                            _hp = value;
                            _hp = _hp < 0 ? 0 : _hp;

                            evtDecLives.Invoke();
                            evtLives.Invoke();
                    }

                }
            }
        }

        internal Vector3 control = Vector3.zero;
        internal bool isJump;
        internal bool isInteractive;

        public TypeAnimation typeAnimation
        { 
            get=>_typeAnimation;
            set 
            {
                if (_typeAnimation != value)
                {
                    evtSetAnimation.Invoke(value);                    
                }
                _typeAnimation = value;
                
            }
        }
        private TypeAnimation _typeAnimation;
        internal event Action<TypeAnimation> evtSetAnimation = delegate { };
        public bool isOnGround;
        public bool isWallFront;

        internal event Action<Commands> evtSetCommand = delegate { };
        private Commands _command;
        public Commands command
        {
            get => _command;
            set
            {
                if (_command != value) evtSetCommand(value);
                    _command = value;
            }
        }

        public int scores
        {
            get => _scores;
            set
            {
                _scores = value;
                evtScores.Invoke();
            }
        }
        private int _scores;
    }

}