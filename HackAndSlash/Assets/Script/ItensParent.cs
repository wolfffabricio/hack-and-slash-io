using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItensParent : MonoBehaviour
{
    protected bool onGround;
    protected bool attacking;
    protected int charges;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        attacking = false;
    }

    public virtual void PickItem()
    {
        onGround = false;
        Debug.Log("Peguei um Item Generico");
    }

   public virtual void UseItem()
    {
        Debug.Log("Usei um Item Generico");
    }
}
