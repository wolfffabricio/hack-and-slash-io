using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    string behaviour= "Looking For Item";// Looking For Item, Looking for Enemy, Attacking

    List<Transform> allGroundItens = new List<Transform>();
    List<Transform> myEnemies = new List<Transform>();

    Transform closestItem;
    Transform closestEnemy;

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

                if(CheckClosestItem())
                {
                    PickItem();
                }
                else
                {
                    MoveToItem();
                }
                break;
            case "Looking for Enemy":
                UpdateMyEnemies();
                //fazer a msm coisa para inimigos

                //pegar referencia do iniimgo mais proximo e ver se estou em range pra bater nele
                //se sim, troca behaviou pra atk, se nao, me aproximo dele
                break;
            case "Attacking":
                UpdateMyEnemies();

                //checar se minha arma não quebrou
                if(gameObject.GetComponent<Enemy>().StillHaveWeapon())
                {
                    gameObject.GetComponent<Enemy>().isAttacking = true;
                }
                else
                {
                    behaviour = "Looking For Item";
                }

                //checar se meu iniimgo não foi mto longe
                break;
        }
    }

    void UpdateAllGroundItens()
    {
        allGroundItens = gameManager.GetGroundItensTransform();
    }

    bool CheckClosestItem()
    {
        float nearestDistance = 999;
        foreach (Transform item in allGroundItens)
        {
            float distance = Vector3.Distance(transform.position, item.position);
            if (distance <= nearestDistance)
            {
                nearestDistance = distance;
                closestItem = item;
            }
        }
        if (nearestDistance < 2.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveToItem()
    {
        float myX = transform.position.x;
        if (myX > closestItem.position.x)
        {
            gameObject.GetComponent<Enemy>().moveX = Random.Range(-1.0f, -0.2f);
        }
        else
        {
            gameObject.GetComponent<Enemy>().moveX = Random.Range(0.2f, 1.0f);
        }

        float myZ = transform.position.z;
        if (myZ > closestItem.position.z)
        {
            gameObject.GetComponent<Enemy>().moveZ = Random.Range(-1.0f, -0.2f);
        }
        else
        {
            gameObject.GetComponent<Enemy>().moveZ = Random.Range(0.2f, 1.0f);
        }
    }

    void PickItem()
    {
        gameObject.GetComponent<Enemy>().isPickingItem = true;

        behaviour = "Looking for Enemy";
    }

    void UpdateMyEnemies()
    {
        myEnemies = gameManager.GetEnemiesTransform(gameObject);
    }
}
