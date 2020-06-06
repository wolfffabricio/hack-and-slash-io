﻿using System.Collections;
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
        
        health = 100+ GameObject.Find("User").GetComponent<User>().HealthBonus;
        powerBonus = GameObject.Find("User").GetComponent<User>().PowerBonus;

        item = null;

        string msg = "Partida começou!";
        msg += " Vida inicial:" + health;
        msg += ". Poder extra:" + powerBonus;
        Debug.Log(msg);
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
