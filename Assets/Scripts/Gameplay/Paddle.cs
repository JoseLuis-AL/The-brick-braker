using System;
using ScriptableObjects.EventsChannelSO;
using UnityEngine;

namespace Gameplay
{
    public class Paddle : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Movement ----------------------------------------------------------------------------------------------------
        [Header("Movement")] [SerializeField] private float speed = 4.0f;
        [SerializeField] private float maxMovement = 2.0f;

        // Events Channels ---------------------------------------------------------------------------------------------
        [Header("Events Channels (Listener)")] [SerializeField]
        private BoolEventChannelSO gameOverChannel;

        // Temp Values -------------------------------------------------------------------------------------------------
        private float _input;
        private Vector3 _position;

        #endregion

        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Update()
        {
            _input = Input.GetAxis("Horizontal");
            _position = transform.position;
            _position.x += _input * speed * Time.deltaTime;

            if (_position.x > maxMovement) _position.x = maxMovement;
            else if (_position.x < -maxMovement) _position.x = -maxMovement;

            transform.position = _position;
        }

        private void OnEnable() => gameOverChannel.OnEventRaised += DisablePaddle;

        private void OnDisable() => gameOverChannel.OnEventRaised -= DisablePaddle;

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        private void DisablePaddle(bool value) => this.enabled = false;

        #endregion
    }
}