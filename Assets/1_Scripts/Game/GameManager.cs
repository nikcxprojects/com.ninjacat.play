using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    
    [Header("UI")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pausePanel;
    
    [Space]
    
    [Header("Platform Generator")]
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private float _stepY;
    [SerializeField] private int _startPlatformCount;
    [SerializeField] private GameObject _platformPrefab;

    [Space]
    
    [Header("Enemy Generator")]
    [SerializeField] private GameObject _prefabEnemy;
    [SerializeField] private float _timeEnemy;
    
    
    private int _stepIndex;
    private int _score;
    private bool _gameOver;

    private void Start()
    {
        GenerateStartPlatforms();
        StartCoroutine(SpawnObject(_prefabEnemy, _timeEnemy));
        _gameOverPanel.SetActive(false);
        Pause(false);
        _gameOver = false;
    }

    private void GenerateStartPlatforms()
    {
        for(var i = 0; i < _startPlatformCount; i++)
            GenerateNewPlatform();
    }
    
    private IEnumerator SpawnObject(GameObject obj, float time)
    {
		
        while (true)
        {
            var spawnPosition = new Vector2(Random.Range(DisplayManager.LeftX, DisplayManager.RightX),
                DisplayManager.TopY + 2);
			
            Instantiate (obj, spawnPosition, Quaternion.identity);
			
            if (_gameOver) break;
            yield return new WaitForSeconds (time);

        }
    }
    
    public void GenerateNewPlatform()
    {
        var randomPosx = Math.Round(Random.Range(DisplayManager.LeftX/2, DisplayManager.RightX/2), 2);
        Vector2 pos = new Vector2((float) randomPosx, _stepIndex * _stepY + _startPos.y);
        var newStep = Instantiate(_platformPrefab, pos, Quaternion.identity);
        newStep.transform.SetParent(transform);
        newStep.tag = "Platform";
        _stepIndex++;
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _gameOver = true;
    }
    
    public void StartNewGame()
    {
        SceneLoader.getInstance().ReloadScene();
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }
    }
}
