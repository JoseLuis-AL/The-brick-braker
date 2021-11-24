using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Brick Color", menuName = "Brick Color", order = 52)]
    public class BricksColorSO : ScriptableObject
    {
        public Color tier1;
        public Color tier2;
        public Color tier3;
        public Color tierDefault;
    }
}
