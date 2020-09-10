using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform player;

    void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x , gameObject.transform.position.y, player.position.z - 9);       
    }
}
