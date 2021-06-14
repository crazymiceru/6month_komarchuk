using System;
using System.Linq;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class AnimationController : IController, IExecute, IInitialization
    {
        private ControlLeak _controlLeak = new ControlLeak("AnimationController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;
        private AnimationCfg _animationCfg;
        private AnimationData _animationData;
        private float _currentFrame;
        private bool _isStop;

        internal AnimationController(UnitM unit, IUnitView unitView, AnimationCfg animationCfg, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _unit.typeAnimation = TypeAnimation.Idle;
            _unit.evtSetAnimation += SetAnimation;
            _animationCfg = animationCfg;
            
        }

        public void Initialization()
        {
            SetAnimation(TypeAnimation.Idle);
        }

        private void SetAnimation(TypeAnimation typeAnimation)
        {
            _animationData = _animationCfg.animationData.Where(ad => ad.typeAnimation == typeAnimation).FirstOrDefault();
            if (_animationData==null) Debug.LogWarning($"Dont set animation {typeAnimation} on {(_unitView as MonoBehaviour).name} object");
            _currentFrame = 0;
            _isStop = false;
            UpdateSprite();
        }

        private void UpdateSprite() => _unitView.objectSpriteRednderer.sprite = _animationData.sprites[(int)_currentFrame];

        public void Execute(float deltaTime)
        {
            Animation(deltaTime);
        }

        private void Animation(float deltaTime)
        {
            if (!_isStop)
            {
                _currentFrame += _animationCfg.speed * deltaTime;
                if (_currentFrame >= _animationData.sprites.Length)
                {
                    if (_animationData.isLoop)
                    {
                        _currentFrame -= _animationData.sprites.Length;
                    }
                    else _isStop = true;
                }
            }
            if (!_isStop) UpdateSprite();
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}