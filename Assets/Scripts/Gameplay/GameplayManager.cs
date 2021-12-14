using DataPersistence;
using ScriptableObjects.EventsChannelSO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Gameplay Elements -------------------------------------------------------------------------------------------
        [Header("Bricks")] [SerializeField] private Brick brickPrefab;
        [SerializeField] private int lineCount = 6;
        [SerializeField] private Rigidbody ballRb;

        // Gameplay Variables ------------------------------------------------------------------------------------------
        private int _remainingBricks;
        private bool _isStarted;
        private bool _isPaused = true;
        private bool _isGameOver;
        private DataManager.PlayerData _player;

        // Events Channels ---------------------------------------------------------------------------------------------
        [Header("Events Channels (Listener)")] [SerializeField]
        private IntEventChannelSO brickDestroyedChannel;

        [SerializeField] private VoidEventChannelSO ballLostEvent;

        [Header("Events Channels (Invoker)")] [SerializeField]
        private BoolEventChannelSO gameOverChannel;

        [Header("Events Channels (Invoker & Listener)")] [SerializeField]
        private VoidEventChannelSO startGameChannel;

        // Ball Initial Values -----------------------------------------------------------------------------------------
        private float _randomDirection = 0;
        private Vector3 _forceDirection;

        #endregion

        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Load Current Player Data --------------------------------------------------------------------------------
            _player = DataManager.LoadPlayerData(DataManager.CurrentPlayerDataFile);
            
            // Gameplay data -------------------------------------------------------------------------------------------
            const float step = 0.6f;
            var perLine = Mathf.FloorToInt(4.0f / step);
            _remainingBricks = 0;

            // Bricks --------------------------------------------------------------------------------------------------
            var pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
            for (var i = 0; i < lineCount; ++i)
            {
                for (var x = 0; x < perLine; ++x)
                {
                    var position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                    brick.SetPointValue(pointCountArray[i]);
                    _remainingBricks += 1;
                }
            }
        }

        private void Update()
        {
            if (_isStarted || _isPaused) return;
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            _isStarted = true;
            _randomDirection = Random.Range(-1.0f, 1.0f);
            _forceDirection = new Vector3(_randomDirection, 1, 0);
            _forceDirection.Normalize();

            ballRb.transform.SetParent(null);
            ballRb.AddForce(_forceDirection * 2.0f, ForceMode.VelocityChange);
        }

        private void OnEnable()
        {
            ballLostEvent.OnEventRaised += BallLost;
            startGameChannel.OnEventRaised += StartGameplay;
            brickDestroyedChannel.OnEventRaised += BrickDestroyed;
        }

        private void OnDisable()
        {
            ballLostEvent.OnEventRaised -= BallLost;
            startGameChannel.OnEventRaised -= StartGameplay;
            brickDestroyedChannel.OnEventRaised -= BrickDestroyed;
        }

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        private void BallLost()
        {
            _isGameOver = true;
            SavePlayer();
            var isWin = _remainingBricks == 0;
            gameOverChannel.RaiseEvent(isWin);
        }

        private void StartGameplay()
        {
            if (_isPaused)
                _isPaused = false;

            if (_isGameOver)
                SceneManager.LoadScene("Gameplay");
        }

        private void BrickDestroyed(int brickPoints)
        {
            _player.score += brickPoints;
            _remainingBricks -= 1;
            
            // Check if is win.
            var isWin = _remainingBricks == 0;
            if (!isWin) return;
            _isGameOver = true;
            SavePlayer();
            gameOverChannel.RaiseEvent(isWin);
        }

        private void SavePlayer()
        {
            DataManager.SavePlayerToScoreboard(_player, DataManager.ScoreboardDataFile);
            _player.score = 0;
            DataManager.SavePlayerData(_player, DataManager.CurrentPlayerDataFile);
        }

        #endregion
    }
}