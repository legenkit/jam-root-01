using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    #region DATA
    [Header("Handler")]
    [SerializeField] Level[] Levels;
    [HideInInspector] public Level _CurrentLevel;


    [Header("Level Ststus")]
    public bool LevelStarted;
    #endregion

    #region Script Initialization
    void Awake()
    {
        instance = this;
        ManageLevel();
        EventSubscription();
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
    private void EventSubscription()
    {
        EventManager.LevelCleared += StageCleardAction;
    }
    private void OnDisable()
    {
        EventManager.LevelCleared -= StageCleardAction;
    }
    #endregion

    #region Script Actions

    #endregion

    #region Public Methods
    public void StartLevel()
    {
        if (!_CurrentLevel.LevelCleared)
        {
            AudioManager.instance.PlayAudioClip(AudioManager.AudioType.Start);
            LevelStarted = true;
        }
    }

    public void ChangeLevel(int LevelIndex, int GateIndex)
    {
        _CurrentLevel.LevelObj.SetActive(false);
        _CurrentLevel = Levels[LevelIndex];
        _CurrentLevel.LevelObj.SetActive(true);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Event Methods
    void StageCleardAction()
    {
        _CurrentLevel.LevelCleared = true;
        LevelStarted = false;
        AudioManager.instance.PlayAudioClip(AudioManager.AudioType.Start);
    }
    #endregion

}
[System.Serializable]
public class Level
{
    public GameObject LevelObj;
    [HideInInspector] public bool LevelCleared = false;
}
