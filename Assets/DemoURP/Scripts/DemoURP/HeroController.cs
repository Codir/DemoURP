using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoURP
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroController : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Transform CameraTransform;
        [SerializeField] private float MoveSpeed;
        [SerializeField] private float WalkJumpDuration;
        [SerializeField] private float WalkJumpHeight;
        [SerializeField] private float RotationSpeed;
        [SerializeField] private float RotationThreshold;

        #endregion

        #region fields

        private Rigidbody _rigidbody;
        private CameraMovingController _cameraMovingController;
        private bool _isTapped;
        private Vector3 _mousePosition;
        private bool _isMoving;

        #endregion

        #region engine methods

        private void Start()
        {
            Init();
        }

        private void FixedUpdate()
        {
            OnMouseMove();
        }

        void Update()
        {
            CheckInputs();
        }

        #endregion

        #region private methods

        private void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _cameraMovingController = new CameraMovingController(CameraTransform, WalkJumpDuration, WalkJumpHeight);
        }

        private void CheckInputs()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                _isTapped = true;
                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isTapped = false;
                _cameraMovingController.SetEnabled(false);
            }
        }

        private void OnMouseMove()
        {
            if (!_isTapped) return;

            Vector3 diff = Input.mousePosition - _mousePosition;
            MoveRigidBody(DiffToDirection(diff.y, float.Epsilon), DiffToDirection(diff.x, RotationThreshold));
        }

        private void MoveRigidBody(int movingDirection, int rotationDirection)
        {
            _isMoving = movingDirection != 0;
            _cameraMovingController.SetEnabled(_isMoving);
            if (_isMoving)
            {
                var force = transform.forward * movingDirection * MoveSpeed * Time.fixedDeltaTime;
                _rigidbody.AddForce(force);
            }

            if (rotationDirection != 0)
            {
                transform.Rotate(Vector3.up, RotationSpeed * rotationDirection * Time.fixedDeltaTime);
            }
        }

        private int DiffToDirection(float value, float threshold)
        {
            if (Mathf.Abs(value) <= threshold)
                return 0;
            if (value > 0)
                return 1;
            return -1;
        }

        #endregion
    }
}