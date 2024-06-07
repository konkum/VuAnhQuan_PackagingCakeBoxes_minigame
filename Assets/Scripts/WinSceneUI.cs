using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneUI : UIBase
{
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private List<Star> _stars;

    public override void Initialized()
    {
        base.Initialized();
        GameManager.Instance.OnWin += star =>
        {
            this.Show();
            DisplayStar(star);
            PlayerProgressionManager.Instance.PlayerProfile.UpdateStar(star);
            PlayerProgressionManager.Instance.PlayerProfile.CompleteCurrentLevel();
            PlayerProgressionManager.Instance.PlayerProfile.UpdateProgress();
            PlayerProgressionManager.Instance.Save();
        };

        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        nextLevelBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(PlayerProgressionManager.Instance.PlayerProfile.CurrentLevelID);
        });
    }

    private void DisplayStar(int star)
    {
        for (int i = 0; i < star; i++)
        {
            _stars[i].OpenStar(true);
        }
    }
}