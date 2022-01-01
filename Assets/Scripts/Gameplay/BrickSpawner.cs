using System.Collections.Generic;
using Data_SO;
using UnityEngine;

namespace Gameplay
{
    public class BrickSpawner : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Brick prefabs.
        [Header("Brick Prefab")] [SerializeField]
        private GameObject brickPrefab;

        // Bricks color data.
        [Header("Bricks Color Data")] [SerializeField]
        private BricksColorSO bricksColorSO;

        // Spawn bricks config.
        [Header("Spawn Config")] [SerializeField]
        private int[] bricksPointsPerLine = { 1, 1, 2, 2, 5, 5 };

        private int _bricksCount;

        // Bricks list.
        private readonly List<GameObject> _bricks = new List<GameObject>();

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        public int SpawnBricks()
        {
            // Destroy all existent bricks.
            DestroyBricks();

            // Bricks config data.
            const float brickStep = 0.6f;
            var brickPerLine = Mathf.FloorToInt(4.0f / brickStep);

            // Bricks Spawns.
            for (var i = 0; i < bricksPointsPerLine.Length; ++i)
            for (var x = 0; x < brickPerLine; ++x)
            {
                // Instantiate the brick GameObject.
                var position = new Vector3(-1.5f + brickStep * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);

                // Set brick value and color.
                brick.GetComponent<Brick>().Initialize(
                    bricksPointsPerLine[i],
                    bricksPointsPerLine[i] switch
                    {
                        1 => bricksColorSO.tier1,
                        2 => bricksColorSO.tier2,
                        5 => bricksColorSO.tier3,
                        _ => bricksColorSO.tierDefault
                    }
                );

                // Set this GameObject as Parent.
                brick.transform.SetParent(transform);

                // Add brick to the list.
                _bricks.Add(brick);
                _bricksCount++;
            }

            return _bricksCount;
        }

        private void DestroyBricks()
        {
            foreach (var brick in _bricks) Destroy(brick.gameObject);
            _bricks.Clear();
            _bricksCount = 0;
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}