using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnAI"))
        {
            Debug.Log(transform.position);
            if (GameObject.Find("RoadSearch").GetComponent<TurnManager>())
            {
                GameObject.Find("RoadSearch").GetComponent<TurnManager>().AddPoint(transform.position);
            }
        }
    }

}
