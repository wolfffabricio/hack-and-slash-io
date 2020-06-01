using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invetory : MonoBehaviour
{
    private List<Jewellery> myJewellery = new List<Jewellery>();
    private List<Jewellery> activeJewellery = new List<Jewellery>();

    private int lootboxAvailable;

    int[,] grid;
    GameObject gridLayoutContent;
    public GameObject prefabImg;
    // Awake is called before the Start
    private void Awake()
    {
        gridLayoutContent = GameObject.Find("GridLayoutContent");
    }

    // Start is called before the first frame update
    void Start()
    {
        Jewellery jewellery = new Jewellery("Anel de Força", "Anel", 5, 0);
        myJewellery.Add(jewellery);

        activeJewellery.Add(myJewellery[0]);

        ReturnAllPowerBonus();
        ReturnAllHealthBonus();
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
            newobj.GetComponent<Image>().color = Random.ColorHSV();

            //TODO - adidionar botão nas joias pra quando tu clicar nelas poder equipar

            newobj.AddComponent<Jewellery>();
            newobj.GetComponent<Jewellery>().SetJewellery(myJewellery[i]);
        }
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
        GameObject.Find("User").GetComponent<User>().PowerBonus = power;
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
