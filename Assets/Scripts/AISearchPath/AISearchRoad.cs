using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISearchRoad : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskTurn;

    NavMeshAgent agent;
    public GameObject mainObject;
    

    public void StartSearchRoad(GameObject start, GameObject end)
    {
        mainObject.transform.position = start.transform.position;
        agent = GetComponentInParent<NavMeshAgent>();
        agent.SetDestination(end.transform.position);
        NavMeshPath path = agent.path;

        Debug.Log(agent.path.corners.Length);
    }
    private void Update()
    {
        if (agent)
        {

            transform.position = agent.transform.position;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnAI"))
        {
            GameObject.Find("Turns").GetComponent<TurnManager>().AddPoint(collision.gameObject.transform.position);
        }
    }

}
