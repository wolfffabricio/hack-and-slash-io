using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform player;
    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPosition.x = player.position.x;
        newPosition.z = player.position.z;
        transform.position = newPosition;
    }
}
