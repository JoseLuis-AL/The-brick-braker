using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameplayUIHandler : MonoBehaviour
    {
        public void GoToStartMenuScene() => SceneManager.LoadScene("StartMenu");
    }
}
