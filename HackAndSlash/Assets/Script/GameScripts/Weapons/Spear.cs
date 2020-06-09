using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : ItensParent
{
    float attackDuration = 0;
    float attackCooldown = 0;
    private void Awake()
    {
        charges = 10;
        onGround = true;
        attacking = false;
        power = 35;

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
        if (attacking)
        {
            attackDuration -= Time.deltaTime;
            if (attackDuration < 0)
            {
                attacking = false;

                if (charges == 0)
                {
                    //TODO destruir item
                    gameObject.GetComponentInParent<PlayersParent>().Item = null;
                    Destroy(this.gameObject);    
                }
                transform.localPosition = new Vector3(0.25f, 0, 0);
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

            transform.localPosition = new Vector3(0.2f, 0, 0);

            transform.localRotation = Quaternion.LookRotation(Vector3.down);

            power += gameObject.GetComponentInParent<PlayersParent>().PowerBonus;

            gameManager.RemoveGroundItem(gameObject);
        }
    }

    public override void UseItem()
    {
        if (!attacking && attackCooldown <= 0)
        {
            charges--;

            attacking = true;

            Vector3 newPosition = new Vector3(0.2f, 0, 1.5f);
            transform.localPosition = newPosition;

            attackDuration = 0.25f;
            attackCooldown = 0.75f;
        }
    }
}
