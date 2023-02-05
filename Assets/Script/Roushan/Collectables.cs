using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Root"))
        {
            Debug.Log("Collision Detected");
            Destroy(this.gameObject);
        }
    }
}
