using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMooving : MonoBehaviour
{
    [SerializeField] private GameObject car;
    void Update()
    {
        transform.position = new Vector3(car.transform.position.x, car.transform.position.y, -157.4f);
    }
}
