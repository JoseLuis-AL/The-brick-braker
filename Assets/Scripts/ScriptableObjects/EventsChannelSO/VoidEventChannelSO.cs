using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.EventsChannelSO
{
    [CreateAssetMenu(menuName = "Event Channel/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public UnityAction OnEventRaised;
        public void RaiseEvent() => OnEventRaised?.Invoke();
    }
}
