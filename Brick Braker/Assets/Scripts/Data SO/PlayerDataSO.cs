using UnityEngine;

namespace Data_SO
{
    [CreateAssetMenu(menuName = "Player Data SO/New Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Player Info")]
        public string playerName;

        public int playerScore;
    }
}