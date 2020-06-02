using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    private int gold;
    private int powerBonus;
    private int healthBonus;
    private Dictionary<string, Jewellery> activeJewellery = new Dictionary<string, Jewellery>();

    public int PowerBonus { get => powerBonus; set => powerBonus = value; }
    public int HealthBonus { get => healthBonus; set => healthBonus = value; }

    private static GameObject thisInstance;

    // Start is called before the first frame update
    void Start()
    {
        if (thisInstance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            thisInstance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ReturnAllPowerBonus()
    {
        int power = 0;
        foreach (KeyValuePair<string, Jewellery> jewell in activeJewellery)
        {
            power += jewell.Value.PowerBonus;
        }
        powerBonus = power;
    }

    private void ReturnAllHealthBonus()
    {
        int health = 0;
        foreach (KeyValuePair<string, Jewellery> jewell in activeJewellery)
        {
            health += jewell.Value.HealthBonus;
        }
        healthBonus = health;
    }

    public void EquipJewell(ref Jewellery j)
    {
        activeJewellery.Add(j.JewellType, j);
        j.IsEquiped = true;

        ReturnAllPowerBonus();
        ReturnAllHealthBonus();
    }

    public void UnequipJewell(Jewellery j)
    {
        activeJewellery.Remove(j.JewellType);
        j.IsEquiped = false;

        ReturnAllPowerBonus();
        ReturnAllHealthBonus();
    }

    public string[] EquippedJewellsName()
    {
        string[] jewellsName = new string[activeJewellery.Count];

        int count = 0;
        foreach (KeyValuePair<string, Jewellery> jewell in activeJewellery)
        {

            jewellsName[count] = jewell.Value.JewellName;
            count++;
        }

        return jewellsName;
    }
}
