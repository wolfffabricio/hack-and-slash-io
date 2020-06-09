using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    string behaviour;// Looking For Item, Looking for Enemy, Attacking

    List<Transform> allGroundItens = new List<Transform>();
    List<Transform> myEnemies = new List<Transform>();

    Transform enemy;

    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        switch (behaviour)
        {
            case "Looking For Item":
                UpdateAllGroundItens();
                break;
            case "Looking for Enemy":
                UpdateMyEnemies();
                break;
            case "Attacking":
                UpdateMyEnemies();
                break;
        }
    }

    void UpdateAllGroundItens()
    {
        allGroundItens = gameManager.GetGroundItensTransform();
    }

    void UpdateMyEnemies()
    {
        myEnemies = gameManager.GetEnemiesTransform(gameObject);
    }
}
