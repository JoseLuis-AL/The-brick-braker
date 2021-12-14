using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.EventsChannelSO
{
    [CreateAssetMenu(menuName = "Event Channel/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        public UnityAction<int> OnEventRaised;
        public void RaiseEvent(int value) => OnEventRaised?.Invoke(value);
    }
}
