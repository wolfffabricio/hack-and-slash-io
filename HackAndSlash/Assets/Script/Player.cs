using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;

    private GameObject item = null;

    public KeyCode useItemKey = KeyCode.Q;
    public KeyCode pickItemKey = KeyCode.E;

    public GameObject Item { get => item; set => item = value; }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRotation();

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

    void PlayerRotation()
    {
        if (Input.GetAxis("Vertical") > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if (Input.GetAxis("Horizontal") < -0.2f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left);
        }
        else if (Input.GetAxis("Vertical") < -0.2f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
        else if (Input.GetAxis("Horizontal") > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
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

                    if (item.GetComponent<ItensParent>().OnGround)
                    {
                        item.transform.SetParent(this.transform);
                        item.GetComponent<ItensParent>().PickItem();
                    }
                    else
                    {
                        //não pode pegar item que outra pessoa estiver segurando
                        item = null;
                    }
                }
                else
                {
                    //ja esta segurando um item
                }
            }
        }
    }
}
