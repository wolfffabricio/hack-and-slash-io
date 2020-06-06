using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ItensParent
{
    float attackDuration = 0;
    float attackCooldown = 0;
    private void Awake()
    {
        charges = 10;
        onGround = true;
        attacking = false;
        power = 30;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }

    public override void UseItem()
    {
        if (!attacking && attackCooldown <= 0)
        {
            charges--;

            attacking = true;

            //TODO - animação de atk
            //transform.localPosition = new Vector3(0.2f, 0, 1.5f);
            gameObject.GetComponent<Animation>().Play();

            attackDuration = 0.25f;
            attackCooldown = 0.75f;
        }
    }
}
