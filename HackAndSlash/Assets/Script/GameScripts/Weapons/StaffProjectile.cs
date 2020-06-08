using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffProjectile : ItensParent
{
    float attackDuration = 3.0f;
    int diretcion;
    private void Awake()
    {
        onGround = false;
        attacking = true;
        power = 40;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Translate(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        attackDuration -= Time.deltaTime;
        if (attackDuration < 0.0f)
        {
            //Destroy(this.gameObject);
        }

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 10);
    }
}
