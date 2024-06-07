using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressionManager : MonoBehaviour
{
    private const string PLAYERKEY = "Player";
    public static PlayerProgressionManager Instance { get; private set; }

    [SerializeField] private PlayerProfile _playerProfile;

    public PlayerProfile PlayerProfile => _playerProfile;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        string data = string.Empty;

        if (PlayerPrefs.HasKey(PLAYERKEY))
        {
            data = PlayerPrefs.GetString(PLAYERKEY);
        }
        else
        {
            Save();
        }

        Load(data);
    }

    public void Load(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            _playerProfile = JsonUtility.FromJson<PlayerProfile>(data);
        }
    }

    public void Save()
    {
        var data = JsonUtility.ToJson(_playerProfile);
        PlayerPrefs.SetString(PLAYERKEY, data);
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

[System.Serializable]
public class PlayerProfile
{
    public string CurrentLevelID = "Level 1";
    public List<Level> levels;

    public void UpdateProgress()
    {
        var index = -1;
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].LevelID == CurrentLevelID && levels[i].IsWin)
            {
                index = i;
                break;
            }
        }

        if (index != -1 && (index + 1) < levels.Count)
        {
            levels[index + 1].IsUnlock = true;
            SetCurrentLevel(levels[index + 1].LevelID);
        }
    }

    public void SetCurrentLevel(string id)
    {
        this.CurrentLevelID = id;
    }

    public void CompleteCurrentLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].LevelID == CurrentLevelID)
            {
                levels[i].IsWin = true;
                break;
            }
        }
    }

    public void UpdateStar(int starAmmount)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].LevelID == CurrentLevelID)
            {
                if (levels[i].Star >= starAmmount)
                {
                    break;
                }
                levels[i].Star = starAmmount;
                break;
            }
        }
    }
}

[System.Serializable]
public class Level
{
    public string LevelID = "Level 1";
    public int LevelNumber = 1;
    public int Star = 0;
    public bool IsUnlock = false;
    public bool IsWin = false;
}