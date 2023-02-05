using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; set;}

    #region VAR
    public bool InputActive = true;

    Vector2 origin;
    RaycastHit2D mouseRay;
    GameObject currentTile;
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance);
        else if (instance == null)
            instance = this;
    }

    void Update()
    {
        MyInput();
    }

    void MyInput()
    {
        if (LevelManager.instance.LevelStarted)
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

            if (InputActive && Input.GetMouseButtonDown(0) && currentTile && currentTile.GetComponent<Tile>().Enteractable && !currentTile.GetComponent<Tile>().Active)
            {
                AudioManager.instance.PlayAudioClip(AudioManager.AudioType.click);
                currentTile.GetComponent<Tile>().DoActivity();

            }
        }
        
    }

    public void InputActivation(bool active)
    {
        InputActive = active;
    }
}
