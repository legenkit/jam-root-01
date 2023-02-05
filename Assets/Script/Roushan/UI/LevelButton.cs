using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public LevelData data;
    public GameObject ConnectedRoot;

    private void Start()
    {
        data.button = this.gameObject;
        data.root = ConnectedRoot;
    }

    public void BringDetailBoard()
    {
        LevelBoard.instance.bringDetailBoard(data); 
    }
}
