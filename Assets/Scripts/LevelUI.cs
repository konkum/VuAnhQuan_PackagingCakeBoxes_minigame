using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : UIBase
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Button _levelBtn;
    [SerializeField] private List<Star> _starList;

    public bool IsUnlocked { get; private set; }

    [SerializeField]
    public override void Initialized()
    {
        base.Initialized();
        _levelText.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(true);
    }

    public void UpdateLevel(bool isUnLocked, int levelCount, string levelID)
    {
        _levelText.gameObject.SetActive(isUnLocked);
        _lockImage.gameObject.SetActive(!isUnLocked);
        _levelText.text = levelCount.ToString();
        IsUnlocked = isUnLocked;
        _levelBtn.interactable = isUnLocked;

        _levelBtn.onClick.AddListener(() =>
        {
            PlayerProgressionManager.Instance.PlayerProfile.SetCurrentLevel(levelID);
            SceneManager.LoadScene(levelID);
        });
    }

    public void UpdateStar(int starAmmout)
    {
        if (starAmmout <= 0)
        {
            return;
        }

        for (int i = 0; i < _starList.Count; i++)
        {
            if (i < starAmmout)
            {
                _starList[i].OpenStar(true);
            }
        }
    }
}

[Serializable]
public class Star
{
    [SerializeField] private Image _star;

    public void OpenStar(bool open)
    {
        _star.gameObject.SetActive(open);
    }
}