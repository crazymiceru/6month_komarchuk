using System;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class ParallaxController : IController, IDisposable, ILateExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("ParallaxController");
        private ListControllers _listControllers;
        private Vector2 _coefficient;
        private Transform _transform;
        private Transform _targetTransform;
        private Vector3 _startTargetPos;
        private Vector2 _repeat;
        private Vector3 _offset;

        internal ParallaxController(Transform transform, Transform targetTransform, Vector2 coefficient, Vector2 repeat, ListControllers listControllers)
        {
            _listControllers = listControllers;
            _coefficient = coefficient;
            _transform = transform;
            _targetTransform = targetTransform;
            _startTargetPos = _targetTransform.position;
            _offset = _targetTransform.position - _transform.position;
            _repeat = repeat;
        }

        public void LateExecute()
        {
            var _addPosition = (Vector3)((_targetTransform.position - _startTargetPos) * _coefficient);
            if (_repeat.x != 0) _addPosition.x = (_addPosition.x + _repeat.x * 100) % _repeat.x;
            if (_repeat.y != 0) _addPosition.y = (_addPosition.x + _repeat.y * 100) % _repeat.y;
            _transform.position = _targetTransform.position - _offset - _addPosition;
        }

        public void Dispose()
        {
            _listControllers.Delete(this);
        }
    }
}