using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    #region DATA
    [Header("Handler")]
    public Level[] Levels;
    [HideInInspector] public Level _CurrentLevel;


    [Header("Level Ststus")]
    public bool LevelStarted;
    #endregion

    #region Script Initialization
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    void ManageLevel()
    {
        _CurrentLevel = Levels[0];

        foreach (Level level in Levels)
        {
            level.LevelObj.SetActive(false);
        }
        _CurrentLevel.LevelObj.SetActive(true);
    }
    #endregion

    #region Script Actions

    #endregion

    #region Public Methods
    public void PrepareLevel(int levelIndex)
    {
        _CurrentLevel = Levels[levelIndex];
        _CurrentLevel.LevelObj.SetActive(true);

        //AudioManager.instance.PlayAudioClip(AudioManager.AudioType.Start);
        PathManager.instance.StartTile = _CurrentLevel.First_Tile;
        if(EventManager.PrepareLevel != null)
        {
            EventManager.PrepareLevel();
        }

        AudioManager.instance.MSpeaker.volume = .5f;
    }

    public void StartLevel()
    {
        LevelStarted = true;
        PathManager.instance.InitializePath();
    }

    public void DeactivateLevel()
    {
        _CurrentLevel.LevelObj.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Event Methods
    public void StageCleardAction(int orb)
    {

        _CurrentLevel.LevelCleared = true;
        LevelStarted = false;
        UIManager.instance.BringUI();
        DataManager.instance.AddOrb(orb);
        //AudioManager.instance.PlayAudioClip(AudioManager.AudioType.Start);
    }
    #endregion

}
[System.Serializable]
public class Level
{
    public GameObject LevelObj;
    public Tile First_Tile;
    [HideInInspector] public bool LevelCleared = false;
}
