using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenCanvasManager : MonoBehaviour
{
    [SerializeField] private TitleSceneUI _titleSceneUI;
    [SerializeField] private HowToPlayUI _howToPlayUI;
    [SerializeField] private LevelSelectUI _levelSelectUI;

    private void Start()
    {
        _titleSceneUI.Initialized();
        _howToPlayUI.Initialized();
        _levelSelectUI.Initialized();
    }
}