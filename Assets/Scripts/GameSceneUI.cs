using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneUI : UIBase
{
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private TextMeshProUGUI timer;

    private float _timeRemaining = 45;
    private bool _timerIsRunning = true;

    public override void Initialized()
    {
        base.Initialized();
        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(PlayerProgressionManager.Instance.PlayerProfile.CurrentLevelID);
        });

        GameManager.Instance.OnWin += star => _timerIsRunning = false;
    }

    private void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = 0;
                _timerIsRunning = false;
                GameManager.Instance.OnLose?.Invoke();
            }
        }
        DisplayTime(_timeRemaining);
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}