using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    private int gold;
    private int powerBonus;
    private int healthBonus;

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
        Debug.Log("p:" + PowerBonus);
        Debug.Log("H:" + HealthBonus);
    }
}
