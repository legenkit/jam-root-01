using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; set; }

    [Header("UI")]
    public int orbcount;

    public string OrbId = "OrbId";

    #region DATA
    [Header("REFERENCE")]
    public GameObject Player;

    [Header("Game Data")]
    public int EnemyCount = 0;

    [Header("Game Status")]
    public bool PlayerDead;

    #endregion

    #region Script Initialization
    private void Awake()
    {
        MakeInstance();
    }
    void MakeInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        orbcount = PlayerPrefs.GetInt(OrbId);
    }

    #endregion

    #region Data Management
    public void AddOrb(int count)
    {
        orbcount += count;
        UIManager.instance.UpdateUI();
        SaveOrb();
    }
    void SaveOrb()
    {
        PlayerPrefs.SetInt(OrbId, orbcount);
    }
    #endregion

    #region Event Method
    void UpdateEnemyCount(int i)
    {
        EnemyCount += i;
        if (EnemyCount == 0 && EventManager.LevelCleared != null)
            EventManager.LevelCleared();
    }
    #endregion
}
