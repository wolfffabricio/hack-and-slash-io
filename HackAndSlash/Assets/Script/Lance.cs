using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : ItensParent
{
    // Start is called before the first frame update
    void Start()
    {
        charges = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PickItem()
    {
        if (onGround)
        {
            onGround = false;
            
            Vector3 newPosition = new Vector3(0, 0, 1);
            transform.localPosition = newPosition;
            
            Debug.Log("Peguei uma lança");
        }
    }

    public override void UseItem()
    {
        charges--;

        Debug.Log("Ataquei com lança");
        attacking = true;

        //TODO wait

        attacking = false;

        if(charges==0)
        {
            //TODO destruir item

        }
    }
}
