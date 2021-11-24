using ScriptableObjects;
using UnityEngine;

namespace Gameplay
{
    public class BrickObject : MonoBehaviour
    {
        public int PointValue;
        public BricksColorSO colors;

        void Start()
        {
            var material = GetComponent<MeshRenderer>().material;

            MaterialPropertyBlock block = new MaterialPropertyBlock();
            switch (PointValue)
            {
                case 1:
                    material.color = colors.tier1;
                    break;
                case 2:
                    material.color = colors.tier2;
                    break;
                case 5:
                    material.color = colors.tier3;
                    break;
                default:
                    material.color = colors.tierDefault;
                    break;
            }
        }
    }
}