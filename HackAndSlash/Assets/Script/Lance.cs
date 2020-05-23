using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : ItensParent
{
    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
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
            Debug.Log("Peguei uma lança");
        }
    }

    public override void UseItem()
    {
        Debug.Log("Ataquei com lança");
    }
}
