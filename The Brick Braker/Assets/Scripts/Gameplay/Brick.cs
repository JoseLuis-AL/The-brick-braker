using System;
using DG.Tweening;
using Plugins.Event_System_SO.Scripts.Base_Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [RequireComponent(typeof(BoxCollider), typeof(MeshRenderer), typeof(AudioSource))]
    public class Brick : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game Events.
        [Header("Game Event (Invoker)")] [SerializeField]
        private IntGameEventSO brickDestroyedEvent;

        // Point.
        [Header("Brick Data")] [SerializeField]
        private int value;

        [SerializeField]
        private float destroyTime = 0.2f;
        
        // Effects.
        [Header("Effects")] [SerializeField]
        private ParticleSystem destroyEffect;
        
        // Sounds.
        private AudioSource _audio;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Collision Methods --------------------------------------------------------------------------------

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Ball")) return;

            // Deactivate the box collider to avoid re destruction.
            GetComponent<BoxCollider>().enabled = false;
            
            // Play destruction animation.
            destroyEffect.Play();
            
            // Play audio.
            _audio = GetComponent<AudioSource>();
            _audio.pitch = Random.Range(1f, 1.5f);
            _audio.Play();

            // Init destroy animation and disable the brick GameObject.
            transform.DOScale(new Vector3(0, 0, 0), destroyTime)
                .OnComplete(() =>
                {
                    brickDestroyedEvent.Invoke(value);
                })
                .Play();
        }

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        public void Initialize(int newValue, Color newColor)
        {
            // Assign new value.
            value = newValue;

            // Assign new color.
            var material = GetComponent<MeshRenderer>().material;
            material.color = newColor;
            
            // Assign new particle colors.
            var effect = destroyEffect.main;
            effect.startColor = new ParticleSystem.MinMaxGradient(new Color(newColor.r, newColor.g, newColor.b, 255));
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}