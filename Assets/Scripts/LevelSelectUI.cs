using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : UIBase
{
    [SerializeField] private Button _backBtn;
    [SerializeField] private LevelUI _levelUIPrefab;
    [SerializeField] private Transform _levelHolder;
    [SerializeField] private List<LevelUI> _levelUIList;

    private List<Level> _levels;

    public override void Initialized()
    {
        base.Initialized();
        _levels = PlayerProgressionManager.Instance.PlayerProfile.levels;
        for (int i = 0; i < _levels.Count; i++)
        {
            var level = Instantiate(_levelUIPrefab, _levelHolder);
            level.UpdateLevel(_levels[i].IsUnlock, _levels[i].LevelNumber, _levels[i].LevelID);
            level.UpdateStar(_levels[i].Star);
            _levelUIList.Add(level);
        }
    }
}