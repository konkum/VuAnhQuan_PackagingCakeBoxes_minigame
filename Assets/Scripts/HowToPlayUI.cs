using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUI : UIBase
{
    [SerializeField] private Button _backBtn;
    [SerializeField] private TitleSceneUI _titleSceneUI;

    public override void Initialized()
    {
        base.Initialized();

        _backBtn.onClick.AddListener(() =>
        {
            this.Hide();
            _titleSceneUI.Show();
        });
    }
}