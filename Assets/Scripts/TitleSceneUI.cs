using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneUI : UIBase
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _howToPlayBtn;
    [SerializeField] private HowToPlayUI _howToPlayUI;
    [SerializeField] private LevelSelectUI _levelSelectUI;

    public override void Initialized()
    {
        base.Initialized();

        _startBtn.onClick.AddListener(() =>
        {
            this.Hide();
            _levelSelectUI.Show();
        });

        _howToPlayBtn.onClick.AddListener(() =>
        {
            this.Hide();
            _howToPlayUI.Show();
        });
    }
}