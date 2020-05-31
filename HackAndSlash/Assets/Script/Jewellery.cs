using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewellery : MonoBehaviour
{
    private bool isEquiped;
    private string name;
    private string type;
    private int powerBonus;
    private int healthBonus;

    public Jewellery(string name, string type, int powerBonus, int healthBonus)
    {
        this.name = name;
        this.type = type;
        this.powerBonus = powerBonus;
        this.healthBonus = healthBonus;
    }

    public bool IsEquiped { get => isEquiped; set => isEquiped = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
