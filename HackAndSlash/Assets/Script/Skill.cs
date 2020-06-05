using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private string skillName;
    string description;
    private int price;
    private bool isEquiped;
    int[] unlockRequirement;

    public string SkillName { get => skillName; set => skillName = value; }
    public string Description { get => description; set => description = value; }
    public int Price { get => price; set => price = value; }
    public bool IsEquiped { get => isEquiped; set => isEquiped = value; }
    public int[] UnlockRequirement { get => unlockRequirement; set => unlockRequirement = value; }

    public Skill(string skillName, string description, int price, bool isEquiped=false, int[] unlockRequirement=null)
    {
        this.skillName = skillName;
        this.description = description;
        this.price = price;
        this.isEquiped = isEquiped;
        this.unlockRequirement = unlockRequirement;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
