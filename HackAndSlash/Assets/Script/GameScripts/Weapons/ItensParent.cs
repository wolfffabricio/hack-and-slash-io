using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItensParent : MonoBehaviour
{
    protected bool onGround;
    protected bool attacking;
    protected int charges;
    protected int power;


    public bool OnGround { get => onGround; set => onGround = value; }
    public bool Attacking { get => attacking; set => attacking = value; }
    public int Power { get => power; set => power = value; }

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
