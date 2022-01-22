using Data_Persistence;
using Data_SO;
using Plugins.Event_System_SO.Scripts;
using Plugins.Event_System_SO.Scripts.Base_Events;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game Events.
        [Header("Game Events (Listener)")] [SerializeField]
        private VoidGameEventSO startGameEvent;

        [SerializeField]
        private IntGameEventSO brickDestroyedEvent;

        [SerializeField]
        private VoidGameEventSO ballLostEvent;

        [SerializeField]
        private StringGameEventSO loadNewSceneEvent;

        [Header("Game Events (Invoker)")] [SerializeField]
        private BoolGameEventSO gameOverEvent;

        [SerializeField]
        private StringGameEventSO transitionToNewSceneEvent;

        // Brick prefabs.
        [Header("Brick Spawner")] [SerializeField]
        private BrickSpawner brickSpawner;

        // Player data.
        [Header("Player Data (Read & Write)")] [SerializeField]
        private PlayerDataSO currentPlayerData;

        private readonly PlayerData _playerData = new PlayerData();

        // Scoreboard data.
        [Header("Persistent Data Files (Read & Write)")] [SerializeField]
        private string scoreboardFile;

        private readonly ScoreboardData _scoreboardData = new ScoreboardData();

        // Gameplay variables.
        private int _bricksRemaining;
        private bool _isGameOver;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            _bricksRemaining = brickSpawner.SpawnBricks();
            _scoreboardData.LoadFromFile(scoreboardFile);
            
            currentPlayerData.playerScore = 0;
        }

        private void OnEnable()
        {
            startGameEvent.RegisterListener(OnStartEvent);
            brickDestroyedEvent.RegisterListener(OnBrickDestroyed);
            ballLostEvent.RegisterListener(OnBallLost);
            loadNewSceneEvent.RegisterListener(OnLoadNewScene);
        }

        private void OnDisable()
        {
            startGameEvent.UnregisterListener(OnStartEvent);
            brickDestroyedEvent.UnregisterListener(OnBrickDestroyed);
            ballLostEvent.UnregisterListener(OnBallLost);
            loadNewSceneEvent.UnregisterListener(OnLoadNewScene);
        }

        #endregion -----------------------------------------------------------------------------------------------------


        #region Callbacks Methods --------------------------------------------------------------------------------------

        private void OnStartEvent()
        {
            if (_isGameOver) transitionToNewSceneEvent.Invoke("Gameplay Scene");
        }

        private void OnBrickDestroyed(int value)
        {
            _bricksRemaining--;
            currentPlayerData.playerScore += value;

            if (_bricksRemaining != 0) return;
            GameOver();
        }

        private void OnBallLost() => GameOver();

        private void OnLoadNewScene(string scene) => transitionToNewSceneEvent.Invoke(scene);

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        private void GameOver()
        {
            _isGameOver = true;

            _playerData.name = currentPlayerData.playerName;
            _playerData.score = currentPlayerData.playerScore;

            _scoreboardData.AddPlayer(_playerData);
            _scoreboardData.SaveToFile(scoreboardFile);

            var isWin = _bricksRemaining == 0;
            gameOverEvent.Invoke(isWin);
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}