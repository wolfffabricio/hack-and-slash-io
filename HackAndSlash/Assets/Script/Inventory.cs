﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    private List<Jewellery> myJewellery = new List<Jewellery>();
    private Dictionary<string, Jewellery> activeJewellery = new Dictionary<string, Jewellery>();

    private int lootboxAvailable;

    User user;

    int[,] grid;
    GameObject gridLayoutContent;
    GameObject prefabImg;
    // Awake is called before the Start
    private void Awake()
    {
        gridLayoutContent = GameObject.Find("GridLayoutContent");

        prefabImg = Resources.Load("Jewellery") as GameObject;

        user = GameObject.Find("User").GetComponent<User>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadString();
        //
        PopulateGrid();
    }

    void PopulateGrid()
    {
        grid = new int[5, Mathf.CeilToInt(myJewellery.Count / 5.0f)];
        GameObject newobj;

        for (int i = 0; i < myJewellery.Count; i++)
        {
            newobj = (GameObject)Instantiate(prefabImg, gridLayoutContent.transform);
            newobj.GetComponent<Image>().color = UnityEngine.Random.ColorHSV();

            newobj.AddComponent<Jewellery>();
            newobj.GetComponent<Jewellery>().SetJewellery(myJewellery[i]);

            InventoryUI inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
            Jewellery j = myJewellery[i];
            newobj.GetComponent<Button>().onClick.AddListener(delegate { inventoryUI.ShowEquipOrUnequipButton(ref j); });
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
            line += jewellery.IsEquiped;
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

                Jewellery jewellery = new Jewellery(lines[0], lines[1], Int32.Parse(lines[2]), Int32.Parse(lines[3]), Convert.ToBoolean(lines[4]));
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
