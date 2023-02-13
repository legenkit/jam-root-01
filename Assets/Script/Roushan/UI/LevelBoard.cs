using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelBoard : MonoBehaviour
{
    public static LevelBoard instance;

    public Image Icon;
    public TextMeshProUGUI detailText;
    public Button Grow;
    public Button okay;
    public GameObject BGButton;

    public int levelindex;


    private void Awake()
    {
        instance = this;
    }


    public void bringDetailBoard(LevelData data)
    {

        Icon.sprite = data.icon;
        levelindex = data.levelIndex;
        if (Tree.instance.Level>=data.treelevel)
        {
            detailText.color = Color.white;
            detailText.SetText(data.resource);
            Grow.gameObject.SetActive(true);
            okay.gameObject.SetActive(false);
        }
        else
        {
            detailText.color = Color.red;
            detailText.SetText($"LEVEL {data.treelevel} TREE REQUIRED");
            Grow.gameObject.SetActive(false);
            okay.gameObject.SetActive(true);
        }

        UIManager.instance.currentLevel = data;

        transform.DOLocalMoveY(0, .5f).SetEase(Ease.OutBack);
        BGButton.SetActive(true);

    }

    public void BringLevel()
    {
        DismisBoard();
        UIManager.instance.PrepareLevel(levelindex);
    }

    public void DismisBoard()
    {
        BGButton.SetActive(false);
        transform.DOLocalMoveY(-800, .5f).SetEase(Ease.OutBack);
    }
}
