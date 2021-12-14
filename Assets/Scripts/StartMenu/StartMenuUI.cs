using System;
using DataPersistence;
using ScriptableObjects.EventsChannelSO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenu
{
    public class StartMenuUI : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // UI Components -----------------------------------------------------------------------------------------------
        [SerializeField] private TMP_InputField inputField;
        
        // Events Channels ---------------------------------------------------------------------------------------------
        [SerializeField] private VoidEventChannelSO loadGameplayEvent;
        [SerializeField] private VoidEventChannelSO loadScoreboardEvent;
        [SerializeField] private VoidEventChannelSO appQuitEvent;

        #endregion
        
        #region Unity Methods ------------------------------------------------------------------------------------------

        private void OnEnable()
        {
            loadGameplayEvent.OnEventRaised += GoToGameplayScene;
            loadScoreboardEvent.OnEventRaised += GoToScoreboardScene;
            appQuitEvent.OnEventRaised += QuitGame;
        }

        private void OnDisable()
        {
            loadGameplayEvent.OnEventRaised -= GoToGameplayScene;
            loadScoreboardEvent.OnEventRaised -= GoToScoreboardScene;
            appQuitEvent.OnEventRaised -= QuitGame;
        }

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        public void GoToGameplayScene()
        {
            var playerData = new DataManager.PlayerData(inputField.text.ToUpper(), 0);
            DataManager.SavePlayerData(playerData, DataManager.CurrentPlayerDataFile);
            SceneManager.LoadScene("Gameplay");
        }

        public void GoToScoreboardScene() => SceneManager.LoadScene("Scoreboard");

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }

        #endregion
    }
}