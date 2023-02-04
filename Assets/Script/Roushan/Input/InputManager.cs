using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputManager : MonoBehaviour
{
    #region VAR
    Vector2 origin;
    RaycastHit2D mouseRay;
    GameObject currentTile;
    #endregion

    void Update()
    {
        MyInput();
    }

    void MyInput()
    {
        origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                          Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        mouseRay = Physics2D.Raycast(origin, Vector2.zero, 0f);
        if (mouseRay && mouseRay.collider.CompareTag("Tile"))
        {
            currentTile = mouseRay.collider.gameObject;
        }
        else
        {
            currentTile = null;
        }

        if (Input.GetMouseButtonDown(0) && currentTile && currentTile.GetComponent<Tile>().Enteractable && !currentTile.GetComponent<Tile>().Active)
        {
            currentTile.GetComponent<Tile>().DoActivity();

        }
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.CompleteGrowth();
        }
    }


}
