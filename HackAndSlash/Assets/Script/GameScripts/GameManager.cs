using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<GameObject> allGroundItens = new List<GameObject>();
    GameObject[] allPlayers = new GameObject[31];

    private void Awake()
    {
        for (int i = 0; i < 30; i++)
        {
            allPlayers[i] = GameObject.Find("Enemy (" + i + ")");
        }
        allPlayers[30] = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGroundItem(GameObject groundItem)
    {
        allGroundItens.Add(groundItem);
    }

    public void RemoveGroundItem(GameObject groundItem)
    {
        allGroundItens.Remove(groundItem);
    }

    public List<Transform> GetGroundItensTransform()
    {
        List<Transform> allGroundItensTransform = new List<Transform>();

        foreach (GameObject go in allGroundItens)
        {
            allGroundItensTransform.Add(go.transform);
        }

        return allGroundItensTransform;
    }

    public List<Transform> GetEnemiesTransform(GameObject thisGameObject)
    {
        List<Transform> myEnemies = new List<Transform>();

        for (int i = 0; i < 31; i++)
        {
            if (allPlayers[i] != null && allPlayers[i] != thisGameObject)
            {
                myEnemies.Add(allPlayers[i].transform);
            }
        }
        return myEnemies;
    }

    public void PlayerDeath()
    {
        int gold = GameObject.Find("User").GetComponent<User>().Gold;
        gold += Mathf.RoundToInt(Time.timeSinceLevelLoad/5);
        GameObject.Find("User").GetComponent<User>().Gold = gold;

        SceneManager.LoadScene(0);
    }
}
