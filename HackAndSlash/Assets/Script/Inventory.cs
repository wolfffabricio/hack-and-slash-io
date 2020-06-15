using Firebase.Database;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    private List<Jewellery> myJewellery = new List<Jewellery>();
    private Dictionary<string, Jewellery> activeJewellery = new Dictionary<string, Jewellery>();

    private int lootboxAvailable;

    User user;

    public List<Jewellery> MyJewellery { get => myJewellery; /*set => myJewellery = value; */}
    public Dictionary<string, Jewellery> ActiveJewellery { get => activeJewellery;/* set => activeJewellery = value;*/ }


    // Awake is called before the Start
    private void Awake()
    {
        user = GameObject.Find("User").GetComponent<User>();

        //ReadString();
        GetInventoryInDatabase();
    }

    // Start is called before the first frame update
    void Start()
    {

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
        user.PowerBonus = power;
    }

    private void ReturnAllHealthBonus()
    {
        int health = 0;
        foreach (KeyValuePair<string, Jewellery> jewell in activeJewellery)
        {
            health += jewell.Value.HealthBonus;
        }
        user.HealthBonus = health;
    }

    public void EquipJewell(Jewellery j)
    {
        if (activeJewellery.ContainsKey(j.JewellType))
        {
            activeJewellery[j.JewellType].IsEquiped = false;
            activeJewellery.Remove(j.JewellType);
        }
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

    public bool BuyLootBox()
    {

        int itemPrice = 10;
        if (user.Gold >= itemPrice)
        {
            user.SpendGold(itemPrice);

            CreateNewJewell();

            return true;
        }
        else
        {
            return false;
        }

    }

    public void CreateNewJewell()
    {
        int jewellTypeRand = UnityEngine.Random.Range(1, 6);
        int level = UnityEngine.Random.Range(1, 8);
        int powerRand = 0;
        int healthRand = 0;
        string jewellType = "";
        switch (jewellTypeRand)
        {
            case 1:
                jewellType = "Anel";
                powerRand = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.0f) * level);
                break;
            case 2:
                jewellType = "Brinco";
                healthRand = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.0f) * level);
                break;
            case 3:
                jewellType = "Coroa";
                powerRand = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.0f) * level);
                break;
            case 4:
                jewellType = "Colar";
                healthRand = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.0f) * level);
                break;
            case 5:
                jewellType = "Broche";
                healthRand = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.0f) * level);
                break;
        }

        string jewellName = jewellType;
        switch (level)
        {
            case 1:
                jewellName += " Simples";
                break;
            case 2:
                jewellName += " Melhorado";
                break;
            case 3:
                jewellName += " de Prata";
                break;
            case 4:
                jewellName += " de Ouro";
                break;
            case 5:
                jewellName += " de Diamante";
                break;
            case 6:
                jewellName += " de Diamante Negro";
                break;
            case 7:
                jewellName += " de Esmeralda";
                break;
        }

        Jewellery newJewell = new Jewellery(jewellName, jewellType, powerRand, healthRand, false, level);
        myJewellery.Add(newJewell);

        WriteString();
        SaveInvetoryInDatabase();
    }

    public void SaveInvetoryInDatabase()
    {
        int count = 1;
        foreach (Jewellery jewellery in myJewellery)
        {
            jewellery.Id = count;
            jewellery.SaveJewellery();
            count += 1;
        }
    }

    public void GetInventoryInDatabase()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("JEWELLERY")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    long children = snapshot.ChildrenCount;

                    string nameFinal = "";
                    string typeFinal = "";
                    int powerFinal = 0;
                    int healthFinal = 0;
                    bool isEquippedFinal = false;
                    int levelFinal = 0;

                    for (int i = 1; i < children; i++)
                    {
                        //NAME
                        FirebaseDatabase.DefaultInstance.GetReference("JEWELLERY/JEWELL_" + i + "/jewellName")
                            .GetValueAsync().ContinueWith(task2 =>
                            {
                                if (task2.IsFaulted)
                                {
                                    // Handle the error...
                                }
                                else if (task2.IsCompleted)
                                {
                                    DataSnapshot snapshot2 = task2.Result;
                                    nameFinal = snapshot2.Value as String;
                                }
                            });

                        //TYPE
                        FirebaseDatabase.DefaultInstance.GetReference("JEWELLERY/JEWELL_" + i + "/jewellType")
                                .GetValueAsync().ContinueWith(task3 =>
                                {
                                    if (task3.IsFaulted)
                                    {
                                        // Handle the error...
                                    }
                                    else if (task3.IsCompleted)
                                    {
                                        DataSnapshot snapshot3 = task3.Result;
                                        typeFinal = snapshot3.Value as String;
                                    }
                                });

                        //POWER
                        FirebaseDatabase.DefaultInstance.GetReference("JEWELLERY/JEWELL_" + i + "/powerBonus")
                                .GetValueAsync().ContinueWith(task4 =>
                                {
                                    if (task4.IsFaulted)
                                    {
                                        // Handle the error...
                                    }
                                    else if (task4.IsCompleted)
                                    {
                                        DataSnapshot snapshot4 = task4.Result;
                                        powerFinal = Convert.ToInt32(snapshot4.Value);
                                    }
                                });

                        //HEALTH
                        FirebaseDatabase.DefaultInstance.GetReference("JEWELLERY/JEWELL_" + i + "/healthBonus")
                                .GetValueAsync().ContinueWith(task5 =>
                                {
                                    if (task5.IsFaulted)
                                    {
                                        // Handle the error...
                                    }
                                    else if (task5.IsCompleted)
                                    {
                                        DataSnapshot snapshot5 = task5.Result;
                                        powerFinal = Convert.ToInt32(snapshot5.Value);
                                    }
                                });

                        //IS EQUIPPED
                        FirebaseDatabase.DefaultInstance.GetReference("JEWELLERY/JEWELL_" + i + "/isEquiped")
                                .GetValueAsync().ContinueWith(task6 =>
                                {
                                    if (task6.IsFaulted)
                                    {
                                        // Handle the error...
                                    }
                                    else if (task6.IsCompleted)
                                    {
                                        DataSnapshot snapshot6 = task6.Result;
                                        isEquippedFinal = Convert.ToBoolean(snapshot6.Value);
                                    }
                                });

                        //LEVEL
                        FirebaseDatabase.DefaultInstance.GetReference("JEWELLERY/JEWELL_" + i + "/level")
                                .GetValueAsync().ContinueWith(task7 =>
                                {
                                    if (task7.IsFaulted)
                                    {
                                        // Handle the error...
                                    }
                                    else if (task7.IsCompleted)
                                    {
                                        DataSnapshot snapshot7 = task7.Result;
                                        levelFinal = Convert.ToInt32(snapshot7.Value);
                                    }
                                });

                        //Criação de vdd so aqui
                        Jewellery jewellery = new Jewellery(nameFinal, typeFinal, powerFinal, healthFinal, isEquippedFinal, levelFinal);
                        myJewellery.Add(jewellery);
                    }
                }
            });
    }

    //chamar ao ganhar um novo equipamento
    public void WriteString()
    {
        string path = "Assets/Resources/MyJewells.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);

        foreach (Jewellery jewellery in myJewellery)
        {
            string line = jewellery.JewellName + "@";
            line += jewellery.JewellType + "@";
            line += jewellery.PowerBonus + "@";
            line += jewellery.HealthBonus + "@";
            line += jewellery.IsEquiped + "@";
            line += jewellery.Level;

            writer.WriteLine(line);
        }

        writer.Close();
    }


    void ReadString()
    {
        string path = "Assets/Resources/MyJewells.txt";

        using (StreamReader sr = new StreamReader(path))
        {
            while (sr.Peek() >= 0)
            {
                string[] lines = sr.ReadLine().Split('@');

                Jewellery jewellery = new Jewellery(lines[0], lines[1], Int32.Parse(lines[2]), Int32.Parse(lines[3]), Convert.ToBoolean(lines[4]), Int32.Parse(lines[5]));
                myJewellery.Add(jewellery);

                if (jewellery.IsEquiped)
                {
                    EquipJewell(jewellery);
                }
            }
            sr.Close();
        }

        ReturnAllPowerBonus();
        ReturnAllHealthBonus();
    }

}
