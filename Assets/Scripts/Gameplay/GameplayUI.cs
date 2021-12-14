using DataPersistence;
using ScriptableObjects.EventsChannelSO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // UI Elements -------------------------------------------------------------------------------------------------
        [Header("UI")] [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI menuTitle;
        [SerializeField] private TextMeshProUGUI controlStartText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private GameObject menuMainPanel;
        [SerializeField] private Color gameOverColor;
        [SerializeField] private Color winColor;
        [SerializeField] private Color startColor;

        // Events Channels ---------------------------------------------------------------------------------------------
        [Header("Events Channels (Listener)")] [SerializeField]
        private BoolEventChannelSO gameOverChannel;

        [SerializeField] private VoidEventChannelSO startGameChannel;
        [SerializeField] private VoidEventChannelSO loadStartMenuChannel;
        [SerializeField] private IntEventChannelSO brickDestroyedChannel;

        // Player Data -------------------------------------------------------------------------------------------------
        private DataManager.PlayerData _playerData;
        
        // Gameplay Conditions -----------------------------------------------------------------------------------------
        private bool _isGameOver;

        #endregion

        #region Unity Methods ------------------------------------------------------------------------------------------

        // Start is called before the first frame update
        private void Start()
        {
            // Load Current Player Data --------------------------------------------------------------------------------
            _playerData = DataManager.LoadPlayerData(DataManager.CurrentPlayerDataFile);

            // Set UI --------------------------------------------------------------------------------------------------
            playerNameText.text = _playerData.name;
            menuMainPanel.SetActive(true);
            menuTitle.text = "Ready?";
            menuTitle.color = startColor;
            scoreText.text = $"Score: {_playerData.score}";
        }

        private void OnEnable()
        {
            gameOverChannel.OnEventRaised += GameOver;
            loadStartMenuChannel.OnEventRaised += GoToStartMenuScene;
            startGameChannel.OnEventRaised += StartGame;
            brickDestroyedChannel.OnEventRaised += UpdateScore;
        }

        private void OnDisable()
        {
            gameOverChannel.OnEventRaised -= GameOver;
            loadStartMenuChannel.OnEventRaised -= GoToStartMenuScene;
            startGameChannel.OnEventRaised -= StartGame;
            brickDestroyedChannel.OnEventRaised -= UpdateScore;
        }

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        private void GoToStartMenuScene() => SceneManager.LoadScene("StartMenu");

        private void StartGame()
        {
            if (!_isGameOver)
                menuMainPanel.SetActive(false);
        }

        private void GameOver(bool isWin)
        {
            if (_isGameOver) return;
            _isGameOver = true;
            
            // Activate Main Menu
            menuMainPanel.SetActive(true);
            
            // Deactivate The Controls text
            controlStartText.gameObject.SetActive(false);
            
            // Activate And Update Final Score Text.
            finalScoreText.gameObject.SetActive(true);
            finalScoreText.text = $"Your score: {_playerData.score}";

            // If Player Win, Set Menu Title To "You Win!" And Change The Color.
            if (isWin)
            {
                menuTitle.text = "You Win!";
                menuTitle.color = winColor;
            }
            // If Player No Win, Set Menu Title To "Game Over" And Change The Color.
            else
            {
                menuTitle.text = "Game Over";
                menuTitle.color = gameOverColor;   
            }
        }

        private void UpdateScore(int value)
        {
            _playerData.score += value;
            scoreText.text = $"Score: {_playerData.score}";
        }

        #endregion
    }
}