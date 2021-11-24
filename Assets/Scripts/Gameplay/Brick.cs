using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class Brick : MonoBehaviour
    {
        public UnityEvent<int> onDestroyed;

        public BricksColorSO colors;
        public int PointValue;

        void Start()
        {
            var material = GetComponent<MeshRenderer>().material;

            switch (PointValue)
            {
                case 1 :
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

        private void OnCollisionEnter(Collision other)
        {
            onDestroyed.Invoke(PointValue);
        
            //slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);
        }
    }
}
