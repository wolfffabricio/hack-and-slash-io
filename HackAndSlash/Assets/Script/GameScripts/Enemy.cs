using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayersParent
{
    bool isAttacking = true;
    bool isPickingItem = true;

    float timerMoveRand = 0;
    float moveX;
    float moveY;
    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = 100;

        item = null;

        bloodParticle = Resources.Load("Particle/Blood_Particle") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMoveRandom();
        PlayersMove();

        PlayersRotation();

        PlayersAttack(isAttacking);

        CheckInvencibiliyTimer();
    }

    void EnemyMoveRandom()
    {
        if (timerMoveRand < 0)
        {
            moveX = Random.Range(-1.2f, 1.2f);
            moveY = Random.Range(-1.2f, 1.2f);

            timerMoveRand = Random.Range(0.2f, 7.5f);
        }
        else
        {
            timerMoveRand -= Time.deltaTime;
        }


        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(moveX, 0.00f, moveY);
            moveDirection *= speed;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        PickItem(collider, isPickingItem);
        TakingDamage(collider);
    }
}
