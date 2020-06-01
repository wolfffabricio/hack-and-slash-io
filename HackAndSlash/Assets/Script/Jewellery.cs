using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewellery : MonoBehaviour
{
    private bool isEquiped;
    private string jewellName;
    private string jewellType;
    private int powerBonus;
    private int healthBonus;

    public Jewellery(string jewellName, string jewellType, int powerBonus, int healthBonus)
    {
        this.jewellName = jewellName;
        this.jewellType = jewellType;
        this.powerBonus = powerBonus;
        this.healthBonus = healthBonus;
    }

    public bool IsEquiped { get => isEquiped; set => isEquiped = value; }
    public int PowerBonus { get => powerBonus; set => powerBonus = value; }
    public int HealthBonus { get => healthBonus; set => healthBonus = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetJewellery(Jewellery jewellery)
    {
        this.jewellName = jewellery.jewellName;
        this.jewellType = jewellery.jewellType;
        this.powerBonus = jewellery.powerBonus;
        this.healthBonus = jewellery.healthBonus;
    }
}
