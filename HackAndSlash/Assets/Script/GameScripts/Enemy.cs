using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayersParent
{
    public bool isAttacking = false;
    public bool isPickingItem = true;

    float timerMoveRand = 0;
    public float moveX;
    public float moveZ;
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
        //EnemyMoveRandom();
        EnemyMove();
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
            moveZ = Random.Range(-1.2f, 1.2f);

            timerMoveRand = Random.Range(0.2f, 7.5f);
        }
        else
        {
            timerMoveRand -= Time.deltaTime;
        }
    }

    void EnemyMove()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(moveX, 0.00f,moveZ);
            moveDirection *= speed;
        }
    }

    public bool StillHaveWeapon()
    {
        if(item!=null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        PickItem(collider, isPickingItem);
        TakingDamage(collider);
    }
}
