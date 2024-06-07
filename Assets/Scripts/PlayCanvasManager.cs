using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCanvasManager : MonoBehaviour
{
    [SerializeField] private GameSceneUI _gameSceneUI;
    [SerializeField] private LoseSceneUI _loseSceneUI;
    [SerializeField] private WinSceneUI _winSceneUI;

    private void Start()
    {
        _gameSceneUI.Initialized();
        _loseSceneUI.Initialized();
        _winSceneUI.Initialized();
    }
}