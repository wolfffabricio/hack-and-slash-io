using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : ItensParent
{
    float attackDuration = 0;
    float attackCooldown = 0;
    GameObject projectile;
    private void Awake()
    {
        charges = 3;
        onGround = true;
        attacking = false;
        power = 40;
        projectile = Resources.Load("Weapons_Prefabs/Staff_Projectile") as GameObject;

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

            transform.localPosition = new Vector3(0.4f, 0, 0);

            transform.localRotation = Quaternion.LookRotation(Vector3.down);

            power += gameObject.GetComponentInParent<PlayersParent>().PowerBonus;
            projectile.GetComponent<StaffProjectile>().Power = power;

            gameManager.RemoveGroundItem(gameObject);

            //ja q o cajado nao ataca diretamente, tem q fazer isso
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public override void UseItem()
    {
        if (attackCooldown <= 0 && !attacking)
        {
            charges--; 

            attacking = true;

            //animação de atk
            //gameObject.GetComponent<Animator>().SetTrigger("Attack");

            Instantiate(projectile, transform.position, transform.parent.rotation);

            attackDuration = 3.0f;
            attackCooldown = 3.0f;

            if (charges == 0)
            {
                //TODO destruir item
                gameObject.GetComponentInParent<PlayersParent>().Item = null;
                Destroy(this.gameObject);
            }
        }
    }
}