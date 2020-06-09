using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayersParent : MonoBehaviour
{
    protected CharacterController characterController;
    protected Vector3 moveDirection = Vector3.zero;

    protected GameObject item;

    protected float speed = 6.0f;
    protected int health;
    protected int powerBonus;

    protected float invencibilityTimer = 0;
    public GameObject Item { get => item; set => item = value; }
    public int PowerBonus { get => powerBonus; set => powerBonus = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void PlayersMove()
    {
        moveDirection.y -= 20.0f * Time.deltaTime;
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    protected void PlayersRotation()
    {
        float v = moveDirection.z;
        float h = moveDirection.x;

        if (v < 0)
        {
            v = v * -1;
        }
        if (h < 0)
        {
            h = h * -1;
        }

        if (v > h)
        {
            if (moveDirection.z > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            }
            else if (moveDirection.z < -0.1f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            }
        }
        else
        {
            if (moveDirection.x > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.right);
            }
            else if (moveDirection.x < -0.1f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.left);
            }
        }
    }

    protected void PlayersAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            //so ataca se tiver segurando item
            if (item != null)
            {
                item.GetComponent<ItensParent>().UseItem();
            }
        }
    }

    protected void PickItem(Collider collider, bool isPickingItem)
    {
        if (collider.tag == "Item" && isPickingItem && item == null)
        {
            if (collider.gameObject.GetComponent<ItensParent>().OnGround)
            {
                item = collider.gameObject;
                item.transform.SetParent(this.transform);
                item.GetComponent<ItensParent>().PickItem();
            }
        }
    }

    protected void TakingDamage(Collider collider)
    {
        if (collider.tag == "Item")
        {
            if (collider.gameObject != item && collider.gameObject.GetComponent<ItensParent>().Attacking && invencibilityTimer <= 0)
            {
                Vector3 knockbackDirection = CheckKnockbackPosition(collider);
                ApplyKnockBack(knockbackDirection);

                invencibilityTimer = 1.0f;

                string damageMsg = this.gameObject.name;
                damageMsg += " foi acertado por um ";
                damageMsg += collider.gameObject.name;
                damageMsg += ". Dano:" + collider.gameObject.GetComponent<ItensParent>().Power;

                Debug.Log(damageMsg);

                health -= collider.gameObject.GetComponent<ItensParent>().Power;
                CheckIfDied();
            }
        }
    }

    protected Vector3 CheckKnockbackPosition(Collider collider)
    {
        Vector3 myPosition = transform.position;
        Vector3 coliderPosition;

        if (collider.transform.parent == null)
        {
            coliderPosition = collider.transform.position;
        }
        else
        {
            coliderPosition = collider.transform.parent.position;
        }

        float difX = coliderPosition.normalized.x - myPosition.normalized.x;
        float difZ = coliderPosition.normalized.z - myPosition.normalized.z;

        if (difX > difZ)
        {
            if (coliderPosition.x > myPosition.x)
            {
                Debug.Log("bateu da direita");
                return Vector3.left;
            }
            else
            {
                Debug.Log("bateu da esquerda");
                return Vector3.right;
            }
        }
        else
        {
            if (coliderPosition.z > myPosition.z)
            {
                Debug.Log("bateu do norte");
                return Vector3.back;
            }
            else
            {
                Debug.Log("bateu do sul");
                return Vector3.forward;
            }
        }
    }

    protected void ApplyKnockBack(Vector3 knockbackDirection)
    {
        transform.Translate(Vector3.up);

        moveDirection = knockbackDirection;
        moveDirection.y = 1.0f;
        moveDirection *= speed;

        PlayersMove();
    }

    protected void CheckInvencibiliyTimer()
    {
        if (invencibilityTimer > 0)
        {
            invencibilityTimer -= Time.deltaTime;
        }
    }

    protected void CheckIfDied()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
