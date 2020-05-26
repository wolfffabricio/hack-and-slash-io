using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : ItensParent
{
    float timeAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        charges = 10;
        onGround = true;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            timeAttack -= Time.deltaTime;
            if (timeAttack < 0)
            {
                attacking = false;

                if (charges == 0)
                {
                    //TODO destruir item
                }
                Vector3 newPosition = new Vector3(0, 0, 0.75f);
                transform.localPosition = newPosition;
            }
        }
    }

    public override void PickItem()
    {
        if (onGround)
        {
            onGround = false;

            Vector3 newPosition = new Vector3(0, 0, 0.75f);
            transform.localPosition = newPosition;

            Debug.Log("Peguei uma lança");
        }
    }

    public override void UseItem()
    {
        if (!attacking)
        {
            charges--;

            attacking = true;

            Debug.Log("Ataquei com lança");

            Vector3 newPosition = new Vector3(0, 0, 1.25f);
            transform.localPosition = newPosition;

            timeAttack = 0.25f;
        }
    }
}
