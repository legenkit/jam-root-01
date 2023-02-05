using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; set; }

    #region VARIABLES
    [Header("References")]
    public RectTransform MainPanel;
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
    void Start()
    {
        
    }
    #endregion


    void Update()
    {
        
    }


    #region Public Methods
    public void MoveToTree()
    {
        MainPanel.DOLocalMove(new Vector3(400,-375,0), .3f).SetEase(Ease.OutBack);
    }
    public void MoveToRoot()
    {
        MainPanel.DOLocalMove(new Vector3(0, 450, 0), .3f).SetEase(Ease.OutBack);
    }
    public void MoveToOption()
    {
        MainPanel.DOLocalMove(new Vector3(-400, -375, 0), .3f).SetEase(Ease.OutBack);
    }

    public void PrepareLevel(int index)
    {
        LevelManager.instance.PrepareLevel(index);
        Sequence S = DOTween.Sequence();
        S.Append(MainPanel.DOLocalMove(new Vector3(-2600,450,0),0.6f).SetEase(Ease.Linear));
        S.AppendCallback(() => LevelManager.instance.StartLevel());

    }
    #endregion
}
