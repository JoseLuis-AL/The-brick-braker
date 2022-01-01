using Plugins.Event_System_SO.Scripts;
using Plugins.Event_System_SO.Scripts.Base_Events;
using UnityEngine;

namespace Gameplay
{
    public class Paddle : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game Events.
        [Header("Game Events (Listener)")] [SerializeField]
        private BoolGameEventSO gameOverEvent;

        [SerializeField]
        private VoidGameEventSO startGameEvent;

        // Movement.
        [Header("Movement")] [SerializeField]
        private float speed = 4.0f;

        [SerializeField]
        private float maxMovement = 1.9f;

        private float _input;
        private bool _canMove;
        private Vector3 _position;

        // Ball Initial values.
        [Header("Ball")] [SerializeField]
        private Rigidbody ballRigidbody;

        private bool _canLaunchTheBall;
        private float _randomDirection;
        private Vector3 _forceDirection;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Update()
        {
            Movement();
            LaunchBall();
        }

        private void OnEnable()
        {
            gameOverEvent.RegisterListener(OnGameOver);
            startGameEvent.RegisterListener(OnStartGame);
        }

        private void OnDisable()
        {
            gameOverEvent.UnregisterListener(OnGameOver);
            startGameEvent.UnregisterListener(OnStartGame);
        }

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        private void OnGameOver(bool _)
        {
            _canMove = false;
        }

        private void Movement()
        {
            if (!_canMove) return;

            // Get horizontal input and current position.
            _input = Input.GetAxis("Horizontal");
            _position = transform.position;

            // Set the new position.
            _position.x += _input * speed * Time.deltaTime;

            // Fix the new position.
            if (_position.x > maxMovement) _position.x = maxMovement;
            if (_position.x < -maxMovement) _position.x = -maxMovement;

            transform.position = _position;
        }

        private void LaunchBall()
        {
            if (!_canLaunchTheBall || ballRigidbody == null) return;
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            _canLaunchTheBall = false;
            _randomDirection = Random.Range(-1f, 1f);
            _forceDirection = new Vector3(_randomDirection, 1, 0);
            _forceDirection.Normalize();

            ballRigidbody.transform.SetParent(null);
            ballRigidbody.AddForce(_forceDirection * 2f, ForceMode.VelocityChange);
        }

        private void OnStartGame()
        {
            _canMove = true;
            _canLaunchTheBall = true;
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}