using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scoreboard
{
    public class ScoreboardMenuUIHandler : MonoBehaviour
    {
        public void GoToStartMenuScene() => SceneManager.LoadScene("StartMenu");
    }
}
