using Plugins.Event_System_SO.Scripts.Base_Events;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider), typeof(AudioSource))]
    public class Ball : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game Events
        [Header("Game Events (Listener)")] [SerializeField]
        private BoolGameEventSO gameOverEvent;

        // Physics.
        private Rigidbody _rigidbody;
        private Vector3 _velocity;
        private const float Acceleration = 0.01f;
        private const float VerticalMultiplier = 0.5f;
        private const float MAXVelocity = 3.0f;
        
        // Sounds.
        private AudioSource _audio;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audio = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            gameOverEvent.RegisterListener(OnGameOver);
        }

        private void OnDisable()
        {
            gameOverEvent.RegisterListener(OnGameOver);
        }

        #endregion -----------------------------------------------------------------------------------------------------
        
        #region Unity Collision Methods --------------------------------------------------------------------------------

        private void OnCollisionEnter(Collision other)
        {
            // Destroy if collides with the DeathZone.
            if (other.collider.CompareTag("DeathZone")) Destroy(gameObject, 0.1f);

            // Get current velocity.
            _velocity = _rigidbody.velocity;

            // After a collision we accelerate a bit.
            _velocity += _velocity.normalized * Acceleration;

            // Check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force.
            if (Vector3.Dot(_velocity.normalized, Vector3.up) < Acceleration)
                _velocity += _velocity.y > 0 ? Vector3.up * VerticalMultiplier : Vector3.down * VerticalMultiplier;

            // Max velocity.
            if (_velocity.magnitude > MAXVelocity) _velocity = _velocity.normalized * MAXVelocity;
            _rigidbody.velocity = _velocity;
            
            // Play sound.
            if (other.collider.CompareTag("Brick")) return;
            _audio.pitch = Random.Range(1f, 1.5f);
            _audio.Play();
        }

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        private void OnGameOver(bool _)
        {
            Destroy(gameObject);
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}