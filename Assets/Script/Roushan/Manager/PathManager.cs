using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathManager : MonoBehaviour
{
    public static PathManager instance { get; set; }

    #region VAR
    [Header("REFERENCE")]
    public GameObject Root;
    public Tile StartTile;
    public Tile lastConnectedTile;
    Tile currentTile;

    public int OrbCollected = 0;
    #region private
    public List<GameObject> path;
    #endregion
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
        InitializeStuffs();
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    void InitializeStuffs()
    {
        //Time.timeScale = 0;
        //InitializePath();
    }
    #endregion

    #region Game Start Methords
    public void StartGame()
    {
        Time.timeScale = 1;
    }
    #endregion

    #region Public Events
    public void InitializePath()
    {
        lastConnectedTile = StartTile ;
        StartTile.Connected = true;
        GrowFurther();
    }
    public void CutTheGrowth(Tile tile)
    {
        if (tile.Connected)
        {
            BuildPath(tile);
            lastConnectedTile = tile;

            Sequence s = DOTween.Sequence();
            foreach (GameObject obj in path)
            {
                if(obj != tile.gameObject)
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.white;
                    s.AppendCallback(() => obj.GetComponent<Tile>().Connected = false);
                    s.Append(obj.transform.DOScale(1.2f, .1f).SetEase(Ease.InBack));    
                    s.Append(obj.transform.DOScale(1, .1f).SetEase(Ease.OutBack));
                    s.Join(obj.transform.GetChild(0).GetComponent<SpriteRenderer>().material.DOColor(Color.white, .1f));
                }
            }
        }
        
    }
    public void GrowFurther()
    {
        if (lastConnectedTile.Connected)
        {
            BuildPath(lastConnectedTile);

            Sequence s = DOTween.Sequence();
            s.AppendCallback(() => InputManager.instance.InputActivation(false));
            foreach (GameObject obj in path)
            {
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

                s.AppendCallback(() => obj.GetComponent<Tile>().Connected = true);
                s.AppendCallback(() => AudioManager.instance.PlayAudioClip(AudioManager.AudioType.Explore));
                s.Append(obj.transform.DOScale(1.2f, .15f).SetEase(Ease.InBack));
                s.Append(obj.transform.DOScale(1, .15f).SetEase(Ease.OutBack));
                s.Join(obj.transform.GetChild(0).GetComponent<SpriteRenderer>().material.DOColor(Color.clear, .2f));
                if(obj.GetComponent<Tile>().type == Tile.TileType.Goal)
                {
                    s.AppendCallback(() => CompleteGrowth());
                }
            }
            s.AppendCallback(() => InputManager.instance.InputActivation(true));
        }
        
    }
    public void CompleteGrowth()
    {
        BuildPath(StartTile);

        foreach (GameObject obj in path)
        {
            obj.GetComponent<SpriteRenderer>().material.color = Color.white;
        }

        Sequence s = DOTween.Sequence();

        s.AppendCallback(() => InputManager.instance.InputActivation(false));
        foreach (GameObject obj in path)
        {
            obj.GetComponent<SpriteRenderer>().color = Color.white;

            s.AppendCallback(() => obj.GetComponent<Tile>().Connected = true);
            s.Append(Root.transform.DOMove(obj.transform.position, .2f).SetEase(Ease.Linear));
            if(obj.GetComponent<Tile>().orb != null)
            {
                s.AppendCallback(() => obj.GetComponent<Tile>().orb.Absorb());
            }
        }
        s.AppendCallback(() => LevelManager.instance.StageCleardAction(OrbCollected));
        s.AppendCallback(() => InputManager.instance.InputActivation(true));
    }

    void BuildPath(Tile pTile)
    {
        path.Clear();
        currentTile = pTile;
        int tries = 0;
        while (currentTile.ActivePathCount() != 0 && tries < 100)
        {
            path.Add(currentTile.gameObject);

            foreach (NeighbourTile tile in currentTile.ConnectedTiles)
            {
                if (tile.Active && !path.Contains(tile.Ntile))
                {
                    currentTile = tile.Ntile.GetComponent<Tile>();
                    break;
                }
            }
            tries++;
        }
        path.Add(currentTile.gameObject);
        lastConnectedTile = currentTile;
    }

    public void AddOrb(int count)
    {
        OrbCollected += count;
    }
    #endregion

    #region Event Methods

    #endregion
}
