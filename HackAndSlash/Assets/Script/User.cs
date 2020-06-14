using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class User : MonoBehaviour
{
    private const string USER_KEY = "USER";
    private const string USER_GOLD = "GOLD";

    private int gold;
    private int powerBonus;
    private int healthBonus;
    private Dictionary<string, Jewellery> activeJewellery = new Dictionary<string, Jewellery>();
    private List<Skill> activeSkills;
    Color playerColor=Color.blue;

    public int Gold { get => gold; set => gold = value; }
    public int PowerBonus { get => powerBonus; set => powerBonus = value; }
    public int HealthBonus { get => healthBonus; set => healthBonus = value; }
    public Color PlayerColor { get => playerColor; set => playerColor = value; }

    private static GameObject thisInstance;

    private void Awake()
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

        GetGoldFromDatabase();
        activeSkills = new List<Skill>();
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

    public void SpendGold(int quantity)
    {
        Gold -= quantity;
        SaveGold();
    }

    public void AddActiveSkill(Skill s)
    {
        activeSkills.Add(s);
        s.IsEquiped = true;
    }

    public void GetGoldFromDatabase()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("USER/GOLD")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    gold = Int16.Parse(snapshot.GetValue(true).ToString());
                }
            });
    }

    public void SaveGold()
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child(USER_KEY).Child(USER_GOLD).SetValueAsync(Gold);
    }
}
