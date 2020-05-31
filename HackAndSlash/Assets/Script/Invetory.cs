using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invetory : MonoBehaviour
{
    private List<Jewellery> myJewellery = new List<Jewellery>();
    private int lootboxAvailable;

    // Start is called before the first frame update
    void Start()
    {
        Jewellery j = new Jewellery("Anel de Força", "Anel", 5, 0);
        myJewellery.Add(j);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
