using Data_SO;
using Plugins.Event_System_SO.Scripts;
using Plugins.Event_System_SO.Scripts.Base_Events;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Main_Menu
{
    public class MainMenuUI : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game events.
        [Header("Game Events (Listener)")] [SerializeField]
        private StringGameEventSO loadNewSceneEvent;

        [SerializeField]
        private VoidGameEventSO appQuitEvent;

        [Header("Game Events (Invoker)")] [SerializeField]
        private StringGameEventSO transitionToNewSceneEvent;

        // UI Components.
        [Header("UI Components")] [SerializeField]
        private TMP_InputField inputField;

        // Player data.
        [Header("Player Data")] [SerializeField]
        private PlayerDataSO currentPlayerData;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Load last player name.
            inputField.text = currentPlayerData.playerName;
        }

        private void OnEnable()
        {
            loadNewSceneEvent.RegisterListener(OnLoadNewScene);
            appQuitEvent.RegisterListener(OnAppQuit);
        }

        private void OnDisable()
        {
            loadNewSceneEvent.UnregisterListener(OnLoadNewScene);
            appQuitEvent.UnregisterListener(OnAppQuit);
        }

        #endregion -----------------------------------------------------------------------------------------------------


        #region Callbacks Mwethods -------------------------------------------------------------------------------------

        private void OnLoadNewScene(string scene)
        {
            // Check for valid scene name.
            if (scene != "Gameplay Scene" && scene != "Scoreboard Scene") return;

            // Set player name and score.
            var playerName = inputField.text == "" ? "UNKNOWN" : inputField.text.ToUpper();
            currentPlayerData.playerName = playerName;
            currentPlayerData.playerScore = 0;

            // Transition to scene.
            transitionToNewSceneEvent.Invoke(scene);
        }

        private void OnAppQuit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}