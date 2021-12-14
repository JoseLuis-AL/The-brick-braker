using System;
using DataPersistence;
using ScriptableObjects.EventsChannelSO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scoreboard
{
    public class ScoreboardUI : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Scoreboard data ---------------------------------------------------------------------------------------------
        private DataManager.ScoreboardData _scoreboardData;

        // Scoreboard entries ------------------------------------------------------------------------------------------
        [Header("Menu Entry")] [SerializeField]
        private GameObject content;

        [SerializeField] private GameObject scoreboardEntry;

        // Events Channels ---------------------------------------------------------------------------------------------
        [Header("Events Channels (Listener)")] [SerializeField]
        private VoidEventChannelSO loadStartMenuEvent;

        #endregion

        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Load scoreboard -----------------------------------------------------------------------------------------
            _scoreboardData = DataManager.LoadScoreboard(DataManager.ScoreboardDataFile);

            // Instantiate the prefabs ---------------------------------------------------------------------------------
            PlayerScoreEntry playerScoreData;
            
            // Player 1.
            var position = scoreboardEntry.transform.position;
            playerScoreData = Instantiate(scoreboardEntry, position, quaternion.identity).GetComponent<PlayerScoreEntry>();
            playerScoreData.transform.SetParent(content.transform);
            playerScoreData.nameText.text = _scoreboardData.Player1.name;
            playerScoreData.scoreText.text = _scoreboardData.Player1.score.ToString();
            
            // Player 2.
            playerScoreData = Instantiate(scoreboardEntry, position, quaternion.identity).GetComponent<PlayerScoreEntry>();
            playerScoreData.transform.SetParent(content.transform);
            playerScoreData.nameText.text = _scoreboardData.Player2.name;
            playerScoreData.scoreText.text = _scoreboardData.Player2.score.ToString();
            
            // Player 3.
            playerScoreData = Instantiate(scoreboardEntry, position, quaternion.identity).GetComponent<PlayerScoreEntry>();
            playerScoreData.transform.SetParent(content.transform);
            playerScoreData.nameText.text = _scoreboardData.Player3.name;
            playerScoreData.scoreText.text = _scoreboardData.Player3.score.ToString();
        }

        private void OnEnable()
        {
            loadStartMenuEvent.OnEventRaised += GoToStartMenuScene;
        }

        private void OnDisable()
        {
            loadStartMenuEvent.OnEventRaised -= GoToStartMenuScene;
        }

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        public void GoToStartMenuScene() => SceneManager.LoadScene("StartMenu");

        #endregion
    }
}