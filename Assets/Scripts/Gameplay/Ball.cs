using UnityEngine;

namespace Gameplay
{
    public class Ball : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Physics -----------------------------------------------------------------------------------------------------
        private Rigidbody _rigidbody;
        private Vector3 _velocity;
        private float _acceleration = 0.01f;
        private float _verticalMultiplier = 0.5f;
        private float _maxVelocity = 3.0f;

        #endregion

        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start() => _rigidbody = GetComponent<Rigidbody>();

        #endregion

        #region Unity Collision Methods --------------------------------------------------------------------------------

        private void OnCollisionExit(Collision other)
        {
            _velocity = _rigidbody.velocity;

            // After a collision we accelerate a bit
            _velocity += _velocity.normalized * _acceleration;

            // Check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
            if (Vector3.Dot(_velocity.normalized, Vector3.up) < _acceleration)
                _velocity += _velocity.y > 0 ? Vector3.up * _verticalMultiplier : Vector3.down * _verticalMultiplier;

            // Max velocity
            if (_velocity.magnitude > _maxVelocity) _velocity = _velocity.normalized * _maxVelocity;
            _rigidbody.velocity = _velocity;
        }

        #endregion
    }
}