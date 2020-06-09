using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    List<GameObject> groundObs = new List<GameObject>();

    private void Awake()
    {
        groundObs.Add(Resources.Load("GroundObjs/Stump") as GameObject);
        groundObs.Add(Resources.Load("GroundObjs/Dandelion") as GameObject);
        groundObs.Add(Resources.Load("GroundObjs/Fern") as GameObject);
        groundObs.Add(Resources.Load("GroundObjs/Mushroom") as GameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            SpawnObj(RandomPrefab(), RandomPosition(), RandomRotation());
        }
    }

    GameObject RandomPrefab()
    {
        return groundObs[Random.Range(0, groundObs.Count)];
    }

    Vector3 RandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-45.0f, 45.1f), 0.0f, Random.Range(-45.0f, 45.1f));

        return position;
    }

    Quaternion RandomRotation()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return Quaternion.LookRotation(Vector3.forward);
            case 2:
                return Quaternion.LookRotation(Vector3.back);
            case 3:
                return Quaternion.LookRotation(Vector3.left);
            default:
                return Quaternion.LookRotation(Vector3.right);

        }
    }

    void SpawnObj(GameObject gameO, Vector3 position, Quaternion rotation)
    {
        if (gameO.name == "Stump")
        {
            position.y = 1.0f;
        }

        Instantiate(gameO, position, rotation, this.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
