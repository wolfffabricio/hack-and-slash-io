using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : PlayersParent
{
    public KeyCode useItemKey = KeyCode.Q;
    public KeyCode pickItemKey = KeyCode.E;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        health = 100 + GameObject.Find("User").GetComponent<User>().HealthBonus;
        powerBonus = GameObject.Find("User").GetComponent<User>().PowerBonus;

        item = null;

        bloodParticle = Resources.Load("Particle/Blood_Particle") as GameObject;

        string msg = "Partida começou!";
        msg += " Vida inicial:" + health;
        msg += ". Poder extra:" + powerBonus;
        Debug.Log(msg);
    }

    // Update is called once per frame
    void Update()
    {
        MoveDirection();
        PlayersMove();
        PlayersRotation();

        PlayersAttack(Input.GetKey(useItemKey));

        CheckInvencibiliyTimer();
    }

    void MoveDirection()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.00f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            //if (Input.GetButton("Jump"))
            //{
            //    moveDirection.y = 8.0f;
            //}
        }
    }

    void OnTriggerStay(Collider collider)
    {
        PickItem(collider, Input.GetKey(pickItemKey));
        TakingDamage(collider);
    }
}
