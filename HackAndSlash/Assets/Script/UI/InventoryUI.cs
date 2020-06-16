using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Button equip, unequip;
    Text[] equippedItemNames = new Text[5];
    Text[] equippedItemPower = new Text[5];
    Text[] equippedItemHealth = new Text[5];
    GameObject gridLayoutContent;
    GameObject prefabImg;

    Text selectedJewellName;
    Text selectedJewellPower;
    Text selectedJewellHealth;
    Text moneyText;

    User user;
    Inventory inventory;

    Jewellery selectedJewellery = null;

    private void Awake()
    {
        equip = GameObject.Find("EquipButton").GetComponent<Button>();
        equip.interactable = false;

        unequip = GameObject.Find("UnequipButton").GetComponent<Button>();
        unequip.interactable = false;

        for (int i = 0; i < 5; i++)
        {
            equippedItemNames[i] = GameObject.Find("EquippedItem" + i).GetComponent<Text>();
            equippedItemPower[i] = GameObject.Find("EquippedItemPower" + i).GetComponent<Text>();
            equippedItemHealth[i] = GameObject.Find("EquippedItemHealth" + i).GetComponent<Text>();
        }

        selectedJewellName = GameObject.Find("TextSelectedJewellName").GetComponent<Text>();
        selectedJewellPower = GameObject.Find("TextSelectedJewellPower").GetComponent<Text>();
        selectedJewellHealth = GameObject.Find("TextSelectedJewellHealth").GetComponent<Text>();
        moneyText = GameObject.Find("TextMoney").GetComponent<Text>();

        user = GameObject.Find("User").GetComponent<User>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        gridLayoutContent = GameObject.Find("GridLayoutContent");
        prefabImg = Resources.Load("Jewellery") as GameObject;

        Vector2 v = new Vector2(Screen.width * 55 / 645, Screen.width * 55 / 645);
        gridLayoutContent.GetComponent<GridLayoutGroup>().cellSize = v;
    }

    /*public*/
    void PopulateGrid(List<Jewellery> myJewellery)
    {
        RestartGrid();

        int[,] grid = new int[5, Mathf.CeilToInt(myJewellery.Count / 5.0f)];
        GameObject newobj;

        for (int i = 0; i < myJewellery.Count; i++)
        {
            newobj = /*(GameObject)*/Instantiate(prefabImg, gridLayoutContent.transform);
            Sprite s = Resources.Load<Sprite>("Jewellery_Images/" + myJewellery[i].JewellType + "_" + myJewellery[i].Level);
            newobj.GetComponent<Image>().sprite = s;

            newobj.AddComponent<Jewellery>();
            newobj.GetComponent<Jewellery>().SetJewellery(myJewellery[i]);

            InventoryUI inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
            Jewellery j = myJewellery[i];
            newobj.GetComponent<Button>().onClick.AddListener(delegate { inventoryUI.ShowEquipOrUnequipButton(ref j); });
        }
    }

    void RestartGrid()
    {
        for (int i = 0; i < gridLayoutContent.transform.childCount; i++)
        {
            Destroy(gridLayoutContent.transform.GetChild(i).GetComponent<Image>());
            Destroy(gridLayoutContent.transform.GetChild(i).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //PopulateGrid(inventory.MyJewellery);
        UpdateEquippedJewellTexts();
        UpdateMoneyText();
    }

    void UpdateEquippedJewellTexts()
    {
        string[] equippedJewellsName = new string[inventory.ActiveJewellery.Count];
        string[] equippedJewellsPower = new string[inventory.ActiveJewellery.Count];
        string[] equippedJewellsHealth = new string[inventory.ActiveJewellery.Count];

        int count = 0;
        foreach (KeyValuePair<string, Jewellery> activeJ in inventory.ActiveJewellery)
        {
            equippedJewellsName[count] = activeJ.Value.JewellName;
            equippedJewellsPower[count] = activeJ.Value.PowerBonus.ToString();
            equippedJewellsHealth[count] = activeJ.Value.HealthBonus.ToString();
            count++;
        }

        for (int i = 0; i < equippedItemNames.Length; i++)
        {
            if (i + 1 > equippedJewellsName.Length)
            {
                equippedItemNames[i].text = "-";
                equippedItemPower[i].text = "-";
                equippedItemHealth[i].text = "-";
            }
            else
            {
                equippedItemNames[i].text = equippedJewellsName[i];
                equippedItemPower[i].text = equippedJewellsPower[i];
                equippedItemHealth[i].text = equippedJewellsHealth[i];
            }
        }
    }

    void UpdateMoneyText()
    {
        moneyText.text = "Dinheiro:" + user.Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //PopulateGrid(inventory.MyJewellery);
        UpdateEquippedJewellTexts();
        UpdateMoneyText();
    }

    public void UpdateGrid()
    {
        PopulateGrid(inventory.MyJewellery);
    }

    public void ShowEquipOrUnequipButton(ref Jewellery j)
    {
        selectedJewellery = j;
        if (selectedJewellery.IsEquiped)
        {
            equip.interactable = false;
            unequip.interactable = true;
        }
        else
        {
            equip.interactable = true;
            unequip.interactable = false;
        }

        selectedJewellName.text = j.JewellName;
        selectedJewellPower.text = "Poder:" + j.PowerBonus;
        selectedJewellHealth.text = "Vida:" + j.HealthBonus;
    }

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void EquipButton()
    {
        inventory.EquipJewell(selectedJewellery);
        ShowEquipOrUnequipButton(ref selectedJewellery);

        UpdateEquippedJewellTexts();
        inventory.WriteString();
        inventory.SaveInvetoryInDatabase();
    }

    public void UnequipButton()
    {
        inventory.UnequipJewell(selectedJewellery);
        ShowEquipOrUnequipButton(ref selectedJewellery);

        UpdateEquippedJewellTexts();
        inventory.WriteString();
        inventory.SaveInvetoryInDatabase();
    }

    public void BuyButton()
    {
        if (inventory.BuyLootBox())
        {
            UpdateMoneyText();

            PopulateGrid(inventory.MyJewellery);
        }
    }
}
