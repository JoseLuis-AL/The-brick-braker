using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenu
{
    public class MenuUIHandler : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        [SerializeField] private TMP_InputField inputField;

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        public void GoToGameplayScene() => SceneManager.LoadScene("Gameplay");

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