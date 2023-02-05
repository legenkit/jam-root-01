using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; set; }

    #region VARIABLES
    [Header("References")]
    public RectTransform MainPanel;

    public LevelData currentLevel;

    [Header("UI Reference")]
    public TextMeshProUGUI TreeText;
    public TextMeshProUGUI OrbText;

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
        UpdateUI();
    }
    #endregion



    #region Public Methods
    public void MoveToTree()
    {
        MainPanel.DOLocalMove(new Vector3(400,-375,0), .3f).SetEase(Ease.OutBack);
    }
    public void focusOnTree()
    {
        MainPanel.DOLocalMove(new Vector3(0,-375,0), .3f).SetEase(Ease.OutBack);
    }
    public void MoveToRoot()
    {
        MainPanel.DOLocalMove(new Vector3(0, 450, 0), .3f).SetEase(Ease.OutBack);
    }
    public void MoveToOption()
    {
        MainPanel.DOLocalMove(new Vector3(-400, -375, 0), .3f).SetEase(Ease.OutBack);
    }

    public void BringUI()
    {

        Sequence S = DOTween.Sequence();
        S.Append( MainPanel.DOLocalMove(new Vector3(0, 450, 0), 1f).SetEase(Ease.OutBack));
        S.AppendCallback(() => LevelManager.instance.DeactivateLevel());
        S.AppendCallback(() => UIManager.instance.AbsorbResource());
    }

    public void PrepareLevel(int index)
    {
        LevelManager.instance.PrepareLevel(index);
        Sequence S = DOTween.Sequence();
        S.Append(MainPanel.DOLocalMove(new Vector3(-2600,450,0),0.6f).SetEase(Ease.Linear));
        S.AppendCallback(() => LevelManager.instance.StartLevel());

    }

    public void AbsorbResource()
    {
        currentLevel.button.transform.DOScale(0, 1).SetEase(Ease.Linear);
    }

    public void UpdateUI()
    {
        TreeText.text = Tree.instance.Level.ToString();
        OrbText.text = DataManager.instance.orbcount.ToString();
    }
    #endregion
}
