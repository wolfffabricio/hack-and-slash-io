using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Button equip, unequip;
    Text[] equppedItens = new Text[5];
    GameObject gridLayoutContent;
    GameObject prefabImg;

    //User user;
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
            equppedItens[i] = GameObject.Find("EquippedItem" + i).GetComponent<Text>();
        }

        //user = GameObject.Find("User").GetComponent<User>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();


        gridLayoutContent = GameObject.Find("GridLayoutContent");
        prefabImg = Resources.Load("Jewellery") as GameObject;

        Vector2 v = new Vector2(Screen.width * 55 / 645, Screen.width * 55 / 645);
        gridLayoutContent.GetComponent<GridLayoutGroup>().cellSize = v;
    }

    public void PopulateGrid(List<Jewellery> myJewellery)
    {
        int[,] grid = new int[5, Mathf.CeilToInt(myJewellery.Count / 5.0f)];
        GameObject newobj;

        for (int i = 0; i < myJewellery.Count; i++)
        {
            newobj = (GameObject)Instantiate(prefabImg, gridLayoutContent.transform);
            Sprite s = Resources.Load<Sprite>("Jewellery_Images/" + myJewellery[i].JewellType + "_" + myJewellery[i].Level);
            newobj.GetComponent<Image>().sprite = s;

            newobj.AddComponent<Jewellery>();
            newobj.GetComponent<Jewellery>().SetJewellery(myJewellery[i]);

            InventoryUI inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
            Jewellery j = myJewellery[i];
            newobj.GetComponent<Button>().onClick.AddListener(delegate { inventoryUI.ShowEquipOrUnequipButton(ref j); });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTexts();
    }

    void UpdateTexts()
    {
        string[] equippedJewellsName = inventory.EquippedJewellsName();

        for (int i = 0; i < equppedItens.Length; i++)
        {
            if (i + 1 > equippedJewellsName.Length)
            {
                equppedItens[i].text = "-";
            }
            else
            {
                equppedItens[i].text = equippedJewellsName[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
    }

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void EquipButton()
    {
        inventory.EquipJewell(selectedJewellery);
        ShowEquipOrUnequipButton(ref selectedJewellery);

        UpdateTexts();
        inventory.WriteString();
    }

    public void UnequipButton()
    {
        inventory.UnequipJewell(selectedJewellery);
        ShowEquipOrUnequipButton(ref selectedJewellery);

        UpdateTexts();
        inventory.WriteString();
    }
}
