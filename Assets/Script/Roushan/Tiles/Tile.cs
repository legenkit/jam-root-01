using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Straight,
        L,
        Goal
    }
    public enum ActivityType
    {
        rotate,
        move
    }
    public bool Enteractable;
    public bool Connected = false;
    [HideInInspector] public bool Active;

    public TileType type;
    public ActivityType Activetype;

    public Collectables orb;

    public float tileSize = 1;
    public NeighbourTile[] ConnectedTiles;

    public Tile[] AffectedTiles;

    [SerializeField] Movement MovePosition;

    #region Script Initialization
    private void Awake()
    {
        EventManager.PrepareLevel += ManageConnection;
    }
    private void OnDisable()
    {
        EventManager.PrepareLevel -= ManageConnection;
    }
    #endregion

    #region Public Methods
    public void ManageConnection()  
    {
        foreach (Tile tile in AffectedTiles)
        {
            tile.ManageConnection();
        }

        foreach (NeighbourTile tile in ConnectedTiles)
        {
            int count = 0;
            foreach (Conditions cond in tile.condition)
            {
                if (cond.CondType == Conditions.Type.rotate)
                {
                    if (cond.conditionObj.localEulerAngles == cond.eulerAngle) count++;
                }
                else if (cond.CondType == Conditions.Type.move)
                {
                    if (cond.conditionObj.localPosition == cond.localPosition) count++;
                }
                else if (cond.CondType == Conditions.Type.both)
                {
                    if (cond.conditionObj.localEulerAngles == cond.eulerAngle && cond.conditionObj.localPosition == cond.localPosition) count++;
                }
            }
            tile.Active = (count == tile.condition.Length);
        }
    }

    public int ActivePathCount()
    {
        int count = 0;
        foreach(NeighbourTile tile in ConnectedTiles)
        {
            if (tile.Active) count++;
        }
        return count;
    }

    public void DoActivity()
    {
        if (Activetype == ActivityType.rotate)
            Rotate();
        else
            Move();
    }

    void Rotate()
    {
        Sequence s = DOTween.Sequence();

        s.AppendCallback(() => PathManager.instance.CutTheGrowth(this));
        s.AppendCallback(() => Activate());
        s.Append(transform.DOLocalRotate(new Vector3(0, 0, 90), 0.3f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack));
        s.AppendCallback(() => ManageConnection());
        s.AppendCallback(() => DeActivate());
        s.AppendCallback(() => PathManager.instance.GrowFurther());
    }
    private void Move()
    {
        Sequence s = DOTween.Sequence();

        s.AppendCallback(() => PathManager.instance.CutTheGrowth(this));
        s.AppendCallback(() => Activate());
        s.Append(transform.DOLocalMove(MovePosition.positions[MovePosition.index++%MovePosition.positions.Length], 0.3f).SetEase(Ease.OutBack));
        s.AppendCallback(() => ManageConnection());
        s.AppendCallback(() => DeActivate());

        if(AffectedTileConnected())s.AppendCallback(() => PathManager.instance.GrowFurther());
    }
    bool AffectedTileConnected()
    {
        bool result = false;
        foreach (Tile item in AffectedTiles)
        {
            if (item.Connected) return true;
        }
        return result;
    }

    public void Activate()
    {
        Active = true;
        this.wait(DeActivate , .3f);
    }
    public void DeActivate()
    {
        Active = false;
    }

    public void connectItSelf(bool conn)
    {
        Connected = conn;
    }

    #endregion

    private void OnDrawGizmos()
    {     
        Gizmos.color =(Enteractable)?Color.red: Color.black;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color =Color.black;

        for (int i=0; i<ConnectedTiles.Length; i++)
        {
            if (ConnectedTiles[i].Active)
            {
                Gizmos.DrawLine(transform.position, ConnectedTiles[i].Ntile.transform.position);
            }
        }

        if (type == TileType.L)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 0.5f);
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        }
    }
}

[System.Serializable]
public class NeighbourTile
{
    public GameObject Ntile;
    public bool Active;
    public Conditions[] condition;
}

[System.Serializable]
public class Conditions
{
    public enum Type
    {
        rotate,
        move,
        both
    }
    public Type CondType;
    public Transform conditionObj;
    public Vector3 eulerAngle;
    public Vector3 localPosition;
}

[System.Serializable]
public class Movement
{
    public Vector3[] positions;
    [HideInInspector] public int index = 0;
}
