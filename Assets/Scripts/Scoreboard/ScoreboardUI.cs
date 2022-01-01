using Data_Persistence;
using Plugins.Event_System_SO.Scripts.Base_Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scoreboard
{
    public class ScoreboardUI : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Scoreboard entries 
        [Header("UI Components")] [SerializeField]
        private GameObject contentParent;

        [SerializeField]
        private GameObject scoreEntry;
        
        // Scoreboard data 
        [Header("Persistent Data Files (Read & Write)")] [SerializeField]
        private string scoreboardFile;

        private readonly ScoreboardData _scoreboard = new ScoreboardData();

        #endregion -----------------------------------------------------------------------------------------------------

        
        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Load scoreboard.
            _scoreboard.LoadFromFile(scoreboardFile);

            // Instantiate players.
            InstantiatePlayerEntry(_scoreboard.player1);
            InstantiatePlayerEntry(_scoreboard.player2);
            InstantiatePlayerEntry(_scoreboard.player3);
        }

        

        #endregion-----------------------------------------------------------------------------------------------------
        
        
        #region Methods ------------------------------------------------------------------------------------------------

        private void InstantiatePlayerEntry(PlayerData playerData)
        {
            var position = scoreEntry.transform.position;
            
            var playerEntry = Instantiate(scoreEntry, position, Quaternion.identity).GetComponent<ScoreboardEntry>();
            playerEntry.transform.SetParent(contentParent.transform);
            playerEntry.nameTMP.text = playerData.name;
            playerEntry.scoreTMP.text = playerData.score.ToString();
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}