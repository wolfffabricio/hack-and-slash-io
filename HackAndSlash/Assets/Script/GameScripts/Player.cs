using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : PlayersParent
{
    public KeyCode useItemKey = KeyCode.Q;
    public KeyCode pickItemKey = KeyCode.E;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        health = 100;
        item = null;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), -1.00f, Input.GetAxis("Vertical"));
        PlayersMove();

        PlayersRotation();

        PlayersAttack(Input.GetKey(useItemKey));

        CheckInvencibiliyTimer();
    }

    void OnTriggerStay(Collider collider)
    {
        PickItem(collider, Input.GetKey(pickItemKey));
        TakingDamage(collider);
    }
}
