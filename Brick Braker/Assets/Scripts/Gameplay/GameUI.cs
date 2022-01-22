using Data_Persistence;
using Data_SO;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameUI : MonoBehaviour
    {
        #region Constants ----------------------------------------------------------------------------------------------

        private const string CurrentScoreText = "Score: ";
        private const string BestScoreText = "Best Score: ";

        #endregion -----------------------------------------------------------------------------------------------------


        #region Attributes ---------------------------------------------------------------------------------------------

        // UI components
        [Header("UI Components")] [SerializeField]
        private TextMeshProUGUI scoreTMP;

        [SerializeField]
        private TextMeshProUGUI bestScoreTMP;

        [SerializeField]
        private TextMeshProUGUI playerNameTMP;

        // Player data.
        [Header("Player Data (Read)")] [SerializeField]
        private PlayerDataSO playerData;
        
        // Scoreboard data.
        [Header("Persistent Data Files (Read)")] [SerializeField]
        private string scoreboardFile;
        private readonly ScoreboardData _scoreboard = new ScoreboardData();

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Scoreboard data.
            _scoreboard.LoadFromFile(scoreboardFile);
            
            // Setup UI.
            playerNameTMP.text = playerData.playerName;
            bestScoreTMP.text = BestScoreText + _scoreboard.highScore;
            scoreTMP.text = CurrentScoreText + 0;
        }

        private void Update()
        {
            scoreTMP.text = CurrentScoreText + playerData.playerScore;
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}