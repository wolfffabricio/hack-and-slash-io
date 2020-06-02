using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayersParent
{
    bool isAttacking=true;
    bool isPickingItem = true;

    float timerMoveRand = 0;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMoveRandom(0.5f);
        PlayersMove();

        PlayersRotation();

        PlayersAttack(isAttacking);

        CheckInvencibiliyTimer();
    }

    void EnemyMoveRandom(float timerToChangeDirection)
    {
        if (timerMoveRand < 0)
        {
            moveDirection = new Vector3(Random.Range(-1.2f, 1.2f), -1.00f, Random.Range(-1.2f, 1.2f));
            timerMoveRand = timerToChangeDirection;
        }
        else
        {
            timerMoveRand -= Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        PickItem(collider, isPickingItem);
        TakingDamage(collider);
    }
}
