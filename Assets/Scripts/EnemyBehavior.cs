using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tr;
    public SpriteRenderer sp;
    GameObject player;

    public float detectionRange = 5;
    public float health=100;
    bool alert = false;

    private void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("deaded");
            Object.Destroy(this.gameObject);
        }
        float xDistance = player.GetComponent<Transform>().position.x - tr.position.x;
        float yDistance = player.GetComponent<Transform>().position.y - tr.position.y;
        float distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);

        if (distance <= detectionRange)
        {
            alert = true;
        }

        if (alert)
        {
            sp.color= Color.yellow;
        }
    }

    public bool TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        return health <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
            rb.velocityY = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.velocityX = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 1;
        }
    }
}
