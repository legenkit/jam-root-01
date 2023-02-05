using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level" , menuName ="new level")]
public class LevelData : ScriptableObject
{
    public Sprite icon;
    public int treelevel;
    public string resource;
    public int levelIndex;
    public GameObject button;
    public GameObject root;
}
