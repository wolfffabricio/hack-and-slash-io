using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewellery : MonoBehaviour
{
    private const string EQUIPMENT_KEY          = "JEWELLERY";
    private const string JEWELLERY_KEY          = "JEWELL";
    private const string JEWELLERY_NAME         = "jewellName";
    private const string JEWELLERY_TYPE         = "jewellType";
    private const string JEWELLERY_POWER_BONUS  = "powerBonus";
    private const string JEWELLERY_HEALTH_BONUS = "healthBonus";
    private const string JEWELLERY_IS_EQUIPED   = "isEquiped";
    private const string JEWELLERY_LEVEL        = "level";

    private string jewellName;
    private string jewellType;
    private int powerBonus;
    private int healthBonus;
    private bool isEquiped;
    private int level;
    private int id;

    public Jewellery(string jewellName, string jewellType, int powerBonus, int healthBonus,bool isEquiped,int level)
    {
        this.jewellName = jewellName;
        this.jewellType = jewellType;
        this.powerBonus = powerBonus;
        this.healthBonus = healthBonus;
        this.isEquiped = isEquiped;
        this.level = level;
    }

    public int Id { get => id; set => id = value; }
    public string JewellName { get => jewellName; set => jewellName = value; }
    public string JewellType { get => jewellType; set => jewellType = value; }
    public int PowerBonus { get => powerBonus; set => powerBonus = value; }
    public int HealthBonus { get => healthBonus; set => healthBonus = value; }
    public bool IsEquiped { get => isEquiped; set => isEquiped = value; }
    public int Level { get => level; set => level = value; }

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

    public void SaveJewellery()
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child(EQUIPMENT_KEY).Child(JEWELLERY_KEY + "_" + Id).Child(JEWELLERY_NAME).SetValueAsync(JewellName);
        FirebaseDatabase.DefaultInstance.RootReference.Child(EQUIPMENT_KEY).Child(JEWELLERY_KEY + "_" + Id).Child(JEWELLERY_TYPE).SetValueAsync(JewellType);
        FirebaseDatabase.DefaultInstance.RootReference.Child(EQUIPMENT_KEY).Child(JEWELLERY_KEY + "_" + Id).Child(JEWELLERY_POWER_BONUS).SetValueAsync(PowerBonus);
        FirebaseDatabase.DefaultInstance.RootReference.Child(EQUIPMENT_KEY).Child(JEWELLERY_KEY + "_" + Id).Child(JEWELLERY_HEALTH_BONUS).SetValueAsync(HealthBonus);
        FirebaseDatabase.DefaultInstance.RootReference.Child(EQUIPMENT_KEY).Child(JEWELLERY_KEY + "_" + Id).Child(JEWELLERY_IS_EQUIPED).SetValueAsync(IsEquiped);
        FirebaseDatabase.DefaultInstance.RootReference.Child(EQUIPMENT_KEY).Child(JEWELLERY_KEY + "_" + Id).Child(JEWELLERY_LEVEL).SetValueAsync(Level);
    }
}
