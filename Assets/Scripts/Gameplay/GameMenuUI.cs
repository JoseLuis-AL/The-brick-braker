using Data_SO;
using DG.Tweening;
using Plugins.Event_System_SO.Scripts;
using Plugins.Event_System_SO.Scripts.Base_Events;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameMenuUI : MonoBehaviour
    {
        #region Constants ----------------------------------------------------------------------------------------------

        // Title text.
        private const string ReadyTitle = "Ready?";
        private const string YouWinTitle = "You win!";
        private const string GameOverTitle = "Game Over";

        // Subtitle text.
        private const string InstructionsSubtitle = "Press spacebar to launch the ball \n use the arrow keys (left & right) for move";
        private const string YourScoreSubtitle = "Your score: ";

        #endregion -----------------------------------------------------------------------------------------------------


        #region Attributes ---------------------------------------------------------------------------------------------

        // Game events.
        [Header("Game Events (Listener)")] [SerializeField]
        private VoidGameEventSO startGameEvent;

        [SerializeField]
        private BoolGameEventSO gameOverEvent;

        [SerializeField]
        private StringGameEventSO loadNewSceneEvent;

        // UI components.
        [Header("UI Components")] [SerializeField]
        private TextMeshProUGUI titleTMP;

        [SerializeField]
        private TextMeshProUGUI subtitleTMP;

        [SerializeField]
        private GameObject mainPanelObject;

        [SerializeField]
        private CanvasGroup mainPanelGroup;

        // UI Colors
        [Header("UI Colors")] [SerializeField]
        private Color gameOverColor;

        [SerializeField]
        private Color winColor;

        [SerializeField]
        private Color readyColor;

        // Player data.
        [Header("Player Data (Read)")] [SerializeField]
        private PlayerDataSO currentPlayerData;

        // Tween.
        private const float TweenDuration = 0.2f;
        private Tween _mainPanelTween;
        
        // Gameplay.
        private bool _gameOver;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Set title and color.
            titleTMP.text = ReadyTitle;
            titleTMP.color = readyColor;

            // Set subtitle.
            subtitleTMP.text = InstructionsSubtitle;
        }

        private void OnEnable()
        {
            startGameEvent.RegisterListener(OnStartGame);
            gameOverEvent.RegisterListener(OnGameOver);
        }

        private void OnDisable()
        {
            startGameEvent.UnregisterListener(OnStartGame);
            gameOverEvent.UnregisterListener(OnGameOver);
        }

        private void OnDestroy()
        {
            KillMainPanelTween();
        }

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        private void OnStartGame()
        {
            if (_gameOver) return;
            
            // Kill the current tween.
            KillMainPanelTween();

            // New tween.
            _mainPanelTween = mainPanelGroup.DOFade(0, TweenDuration);
            _mainPanelTween.OnComplete(() => { mainPanelObject.SetActive(false); });
            _mainPanelTween.Play();
        }

        private void OnGameOver(bool isWin)
        {
            _gameOver = true;
            
            // Activate the main panel.
            mainPanelObject.SetActive(true);

            // Set title and color.
            if (isWin)
            {
                titleTMP.text = YouWinTitle;
                titleTMP.color = winColor;
            }
            else
            {
                titleTMP.text = GameOverTitle;
                titleTMP.color = gameOverColor;
            }

            // Set subtitle.
            subtitleTMP.text = YourScoreSubtitle + currentPlayerData.playerScore;

            // Kill the current tween.
            KillMainPanelTween();

            // Reset main panel size.
            _mainPanelTween = mainPanelGroup.DOFade(1, TweenDuration);
            _mainPanelTween.Play();
        }

        private void KillMainPanelTween()
        {
            if (_mainPanelTween != null && _mainPanelTween.IsActive()) _mainPanelTween.Kill();
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}