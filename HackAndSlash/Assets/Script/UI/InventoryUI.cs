using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Button equip, unequip;
    Text[] equppedItens = new Text[5];

    User user;
    Inventory inventory;

    Jewellery selectedJewellery = null;

    // Start is called before the first frame update
    void Start()
    {
        equip = GameObject.Find("EquipButton").GetComponent<Button>();
        equip.interactable = false;

        unequip = GameObject.Find("UnequipButton").GetComponent<Button>();
        unequip.interactable = false;

        for (int i = 0; i < 5; i++)
        {
            equppedItens[i] = GameObject.Find("EquippedItem" + i).GetComponent<Text>();
        }

        user = GameObject.Find("User").GetComponent<User>();
        inventory=GameObject.Find("Inventory").GetComponent<Inventory>();

        UpdateTexts();
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
