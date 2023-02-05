using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour
{
    public static Tree instance { get; set; }

    public int Level = 0;
    public TreeData[] Trees;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        foreach(TreeData spt in Trees)
        {
            spt.tree.GetComponent<SpriteRenderer>().material.DOColor(Color.clear, .01f);
            spt.Root.GetComponent<SpriteRenderer>().material.DOColor(Color.clear, .01f);
        }

        Trees[Level].tree.GetComponent<SpriteRenderer>().material.DOColor(Color.white, .01f);
        Trees[Level].Root.GetComponent<SpriteRenderer>().material.DOColor(Color.white   , .01f);
    }

    public void UpgradeTree()
    {
        if (DataManager.instance.orbcount >= Trees[Level].cost)
        {
            DataManager.instance.AddOrb(-Trees[Level].cost);

            Sequence s = DOTween.Sequence();
            UIManager.instance.focusOnTree();
            s.Append(Trees[Level].tree.GetComponent<SpriteRenderer>().material.DOColor(Color.clear, 1.5f));
            s.Append(Trees[Level+1].tree.GetComponent<SpriteRenderer>().material.DOColor(Color.white, 1.5f));
            s.AppendCallback(() => UIManager.instance.MoveToRoot());
            s.Append(Trees[Level].Root.GetComponent<SpriteRenderer>().material.DOColor(Color.clear, 1.5f));
            s.Append(Trees[Level+1].Root.GetComponent<SpriteRenderer>().material.DOColor(Color.white, 1.5f));
            s.AppendCallback(() => Level++);
            s.AppendCallback(() => UIManager.instance.UpdateUI());


        }
    }

}
[System.Serializable]
public class TreeData
{
    public GameObject tree;
    public GameObject Root;
    public int cost;
}
