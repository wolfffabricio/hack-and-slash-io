using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;

    private GameObject item = null;

    public KeyCode useItemKey = KeyCode.Q;
    public KeyCode pickItemKey = KeyCode.E;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        PlayerAttack();
    }

    void PlayerMove()
    {
        // Move direction directly from axes
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), -1.00f, Input.GetAxis("Vertical"));
        moveDirection *= speed;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void PlayerAttack()
    {
        if (Input.GetKey(useItemKey))
        {
            //so ataca se tiver segurando item
            if (item != null)
            {
                item.GetComponent<ItensParent>().UseItem();
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Item")
        {
            if (Input.GetKey(pickItemKey))
            {
                if (item == null)
                {
                    item = collider.gameObject;

                    item.transform.SetParent(this.transform);
                    item.GetComponent<ItensParent>().PickItem();
                }
                else
                {
                    Debug.Log("Ja esta segurando um item");
                }
            }
        }
    }
}
