using Plugins.Event_System_SO.Scripts;
using UnityEngine;

namespace Gameplay
{
    public class DeathZone : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game events.
        [Header("Game Events (Invoker)")] [SerializeField]
        private VoidGameEventSO ballLostEvent;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Collision Methods --------------------------------------------------------------------------------

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ball")) return;

            ballLostEvent.Invoke();
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}