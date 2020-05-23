using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;

    private GameObject item = null;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move direction directly from axes
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), -1.00f, Input.GetAxis("Vertical"));
        moveDirection *= speed;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Item")
        {
            if (item == null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    item = collider.gameObject;
                    item.GetComponent<Lance>().PickItem();

                    item.transform.SetParent(this.transform);

                    Vector3 newPosition = new Vector3(0, 0, 1);
                    item.transform.localPosition = newPosition;
                }
            }
        }
    }
}
