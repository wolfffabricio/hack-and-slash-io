using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invetory : MonoBehaviour
{
    private List<Jewellery> myJewellery = new List<Jewellery>();
    private List<Jewellery> activeJewellery = new List<Jewellery>();
    private int lootboxAvailable;

    // Start is called before the first frame update
    void Start()
    {
        Jewellery j = new Jewellery("Anel de Força", "Anel", 5, 0);
        myJewellery.Add(j);

        activeJewellery.Add(myJewellery[0]);

        ReturnAllPowerBonus();
        ReturnAllHealthBonus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ReturnAllPowerBonus()
    {
        int power = 0;
        foreach (Jewellery jewell in activeJewellery)
        {
            power += jewell.PowerBonus;

        }
        GameObject.Find("User").GetComponent<User>().PowerBonus=power;
    }

    private void ReturnAllHealthBonus()
    {
        int health = 0;
        foreach (Jewellery jewell in activeJewellery)
        {
            health += jewell.HealthBonus;
        }
        GameObject.Find("User").GetComponent<User>().HealthBonus = health;
    }
}
