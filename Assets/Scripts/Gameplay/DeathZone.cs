using ScriptableObjects.EventsChannelSO;
using UnityEngine;

namespace Gameplay
{
    public class DeathZone : MonoBehaviour
    {
        [Header("Events Channel (Invoker)")] [SerializeField]
        private VoidEventChannelSO ballLostChannel;
        
        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
            ballLostChannel.RaiseEvent();
        }
    }
}