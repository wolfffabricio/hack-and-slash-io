using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSpawner : MonoBehaviour
{
    List<GameObject> weapons = new List<GameObject>();
    float timer = 0;
    private void Awake()
    {
        weapons.Add(Resources.Load("Weapons_Prefabs/Elven_Spear") as GameObject);
        weapons.Add(Resources.Load("Weapons_Prefabs/Elven_Axe") as GameObject);
        weapons.Add(Resources.Load("Weapons_Prefabs/Elven_Staff") as GameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            SpawnWeapon(RandomPrefab(), RandomPosition(), RandomRotation());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTimer())
        {
            SpawnWeapon(RandomPrefab(), RandomPosition(), RandomRotation());
        }
    }

    GameObject RandomPrefab()
    {
        return weapons[Random.Range(0, weapons.Count)];

    }

    Vector3 RandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-45.0f, 45.1f), 0.25f, Random.Range(-45.0f, 45.1f));

        return position;
    }

    Quaternion RandomRotation()
    {
        if (Random.Range(1, 3) == 1)
        {
            return Quaternion.LookRotation(Vector3.up);
        }
        else
        {
            return Quaternion.LookRotation(Vector3.down);
        }
    }

    void SpawnWeapon(GameObject gameO, Vector3 position, Quaternion rotation)
    {
        Instantiate(gameO, position, rotation, this.transform);
    }

    bool checkTimer()
    {
        if (timer <= 0)
        {
            timer = 5.0f;
            return true;
        }
        else
        {
            timer -= Time.deltaTime;
            return false;
        }
    }
}
