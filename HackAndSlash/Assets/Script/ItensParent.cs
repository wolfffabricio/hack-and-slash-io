using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class ItensParent : MonoBehaviour
{
    protected bool onGround;

    public virtual void PickItem()
    {
        onGround = false;
    }

   public virtual void UseItem()
    {
        
    }
}
