using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.EventsChannelSO
{
    [CreateAssetMenu(menuName = "Event Channel/Bool Event Channel")]
    public class BoolEventChannelSO : ScriptableObject
    {
        public UnityAction<bool> OnEventRaised;
        public void RaiseEvent(bool value) => OnEventRaised?.Invoke(value);
    }
}
