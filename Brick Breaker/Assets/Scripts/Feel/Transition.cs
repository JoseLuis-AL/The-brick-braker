using System;
using DG.Tweening;
using Plugins.Event_System_SO.Scripts.Base_Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feel
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Transition : MonoBehaviour
    {
        #region Attributes ---------------------------------------------------------------------------------------------

        // Game Events.
        [Header("Game Events (Listener)")] [SerializeField]
        private StringGameEventSO transitionToNewSceneEvent;

        // Play animation when the scene is loaded.
        [Space] [SerializeField]
        private bool playOnLoad;

        // Tween.
        [Space] [SerializeField]
        private float duration = 0.2f;

        private Tween _tween;

        // Canvas group.
        private CanvasGroup _transitionScreen;

        #endregion -----------------------------------------------------------------------------------------------------


        #region Unity Methods ------------------------------------------------------------------------------------------

        private void Start()
        {
            // Get the canvas group.
            _transitionScreen = GetComponent<CanvasGroup>();

            // Set tween duration time.
            float tweenDuration;
            if (playOnLoad) tweenDuration = duration;
            else tweenDuration = 0;
            
            _tween = _transitionScreen.DOFade(0, tweenDuration);
            _tween.Play();
        }

        private void OnEnable() => transitionToNewSceneEvent.RegisterListener(OnLoadNewScene);

        private void OnDisable() => transitionToNewSceneEvent.UnregisterListener(OnLoadNewScene);

        #endregion -----------------------------------------------------------------------------------------------------


        #region Callback Methods ---------------------------------------------------------------------------------------

        private void OnLoadNewScene(string scene)
        {
            // Kill the current tween.
            if (_tween.IsActive() && _tween.IsPlaying()) _tween.Kill(false);

            // Set new tween.
            _tween = _transitionScreen.DOFade(1, duration);
            _tween.OnComplete(() => { SceneManager.LoadScene(scene); });
            _tween.Play();
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}