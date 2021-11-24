using System.Collections.Generic;
using UnityEngine;

namespace EventsSO
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Raise()
        {
            foreach (var listener in _listeners)
                listener.OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);

        public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
    }
}