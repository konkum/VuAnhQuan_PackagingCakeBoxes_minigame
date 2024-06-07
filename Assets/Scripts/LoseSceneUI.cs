using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseSceneUI : UIBase
{
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button restartBtn;

    public override void Initialized()
    {
        base.Initialized();
        GameManager.Instance.OnLose += () =>
        {
            this.Show();
        };

        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(PlayerProgressionManager.Instance.PlayerProfile.CurrentLevelID);
        });
    }
}