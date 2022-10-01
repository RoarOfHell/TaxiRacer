using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asd");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("asd");
    }
}
