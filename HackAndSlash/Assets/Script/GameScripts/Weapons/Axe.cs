using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ItensParent
{
    float attackDuration = 0;
    float attackCooldown = 0;
    private void Awake()
    {
        charges = 15;
        onGround = true;
        attacking = false;
        power = 20;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager.AddGroundItem(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        AttackDuration();
        AttackCooldown();
    }

    private void AttackDuration()
    {
        if (attacking == true)
        {
            attackDuration -= Time.deltaTime;
            if (attackDuration < 0.0f)
            {
                attacking = false;

                if (charges == 0)
                {
                    //TODO destruir item
                    gameObject.GetComponentInParent<PlayersParent>().Item = null;
                    Destroy(this.gameObject);
                }

                transform.localPosition = new Vector3(0, 0, 0.45f);
            }
        }
    }

    void AttackCooldown()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public override void PickItem()
    {
        if (onGround)
        {
            onGround = false;

            transform.localPosition = new Vector3(0, 0, 0.45f);

            transform.localRotation = Quaternion.LookRotation(Vector3.down);

            power += gameObject.GetComponentInParent<PlayersParent>().PowerBonus;

            gameManager.RemoveGroundItem(gameObject);
        }
    }

    public override void UseItem()
    {
        if (attackCooldown <= 0 && !attacking)
        {
            charges--;

            attacking = true;

            //animação de atk
            gameObject.GetComponent<Animator>().SetTrigger("Attack");

            attackDuration = 0.5f;
            attackCooldown = 0.5f;
        }
    }
}
