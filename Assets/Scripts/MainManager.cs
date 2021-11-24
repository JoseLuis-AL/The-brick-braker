using System.Collections;
using System.Collections.Generic;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    #region Attributes
    
    // Gameplay Elements -----------------------------------------------------------------------------------------------
    [Header("Prefabs")]
    public Brick brickPrefab;
    public int lineCount = 6;
    public Rigidbody ballRb;

    // UI Elements -----------------------------------------------------------------------------------------------------
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    
    // Gameplay Variables ----------------------------------------------------------------------------------------------
    private bool _isStarted = false;
    private bool _isGameOver = false;
    private int _points = 0;
    
    // Temporal Values -------------------------------------------------------------------------------------------------
    private float _randomDirection = 0;
    private Vector3 _forceDirection;

    #endregion

    
    // Start is called before the first frame update
    private void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        
        if (!_isStarted)
        {
            _isStarted = true;
            _randomDirection = Random.Range(-1.0f, 1.0f);
            _forceDirection = new Vector3(_randomDirection, 1, 0);
            _forceDirection.Normalize();

            ballRb.transform.SetParent(null);
            ballRb.AddForce(_forceDirection * 2.0f, ForceMode.VelocityChange);
        }
        else if (_isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void AddPoint(int point)
    {
        _points += point;
        scoreText.text = $"Score : {_points}";
    }

    public void GameOver()
    {
        _isGameOver = true;
        gameOverPanel.SetActive(true);
    }
}
