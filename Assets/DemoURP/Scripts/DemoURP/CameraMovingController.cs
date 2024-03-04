using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DemoURP
{
    public class CameraMovingController
    {
        private enum MovingState
        {
            Down,
            Up
        }

        #region fields

        private readonly Transform _transform;
        private readonly float _duration;
        private readonly float _maxHeight;
        private readonly float _baseHeight;

        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;
        private MovingState _state;
        private bool _isEnabled;
        private bool _isTweenActive;

        #endregion

        #region engine methods

        public CameraMovingController(Transform transform, float duration, float moveHeight)
        {
            _transform = transform;
            _duration = duration;
            _baseHeight = _transform.position.y;
            _maxHeight = _baseHeight + moveHeight;
        }

        ~CameraMovingController()
        {
            _tween?.Kill();
        }

        #endregion

        #region public methods

        public void SetEnabled(bool value)
        {
            _isEnabled = value;

            if (_isEnabled)
            {
                SetState(MovingState.Up);
            }
        }

        #endregion

        #region private methods

        private void SetState(MovingState state)
        {
            if (_state == state || _isTweenActive) return;

            _tween = state switch
            {
                MovingState.Up => _transform.DOMoveY(_maxHeight, _duration).OnComplete(OnCompeteState),
                MovingState.Down => _transform.DOMoveY(_baseHeight, _duration).OnComplete(OnCompeteState),
                _ => throw new ArgumentOutOfRangeException()
            };

            _isTweenActive = true;
            _state = state;
        }

        private void OnCompeteState()
        {
            _isTweenActive = false;

            if (_state == MovingState.Down && _isEnabled)
            {
                SetState(MovingState.Up);
            }
            else if (_state == MovingState.Up)
            {
                SetState(MovingState.Down);
            }
        }

        #endregion
    }
}