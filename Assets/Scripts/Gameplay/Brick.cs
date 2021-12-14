using System;
using ScriptableObjects;
using ScriptableObjects.EventsChannelSO;
using UnityEngine;

namespace Gameplay
{
    public class Brick : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Events Channels ---------------------------------------------------------------------------------------------
        [Header("Events Channels (Invoker)")] [SerializeField]
        private IntEventChannelSO brickDestroyedChannel;
        
        // Points ------------------------------------------------------------------------------------------------------
        [SerializeField] private int pointValue;
        
        // Colors ------------------------------------------------------------------------------------------------------
        [SerializeField] private BricksColorSO colors;

        #endregion

        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            var material = GetComponent<MeshRenderer>().material;
            material.color = pointValue switch
            {
                1 => colors.tier1,
                2 => colors.tier2,
                5 => colors.tier3,
                _ => colors.tierDefault
            };
        }

        #endregion

        #region Unity Collision Methods --------------------------------------------------------------------------------

        private void OnCollisionEnter(Collision other)
        {
            brickDestroyedChannel.RaiseEvent(pointValue);
            
            // Slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);
        }

        #endregion
        
        #region Methods ------------------------------------------------------------------------------------------------

        public void SetPointValue(int value) => pointValue = value;

        #endregion
    }
}