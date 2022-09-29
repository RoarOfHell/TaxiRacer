using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = Vector3.Lerp(transform.position, player.transform.position, 0.1f);
        Quaternion quaternion = Quaternion.Lerp(transform.rotation, player.transform.rotation, 0.1f);
        targetPosition.z = -10;
        transform.position = targetPosition;
        transform.rotation = quaternion;
    }
}
