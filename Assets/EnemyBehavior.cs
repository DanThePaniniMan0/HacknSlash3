using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tr;
    public SpriteRenderer sp;
    Vector2 move;
    public Animator anim;
    GameObject player;

    public float detectionRange = 5;
    public float health = 100;
    public float speed = 200;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
 

    private void Update()
    {
        move = new Vector2(player.GetComponent<Rigidbody2D>().position.x - rb.GetComponent<Rigidbody2D>().position.x, 0);
    }
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("deaded");
            
            Object.Destroy(this.gameObject);
        }
        float distance = player.GetComponent<Rigidbody2D>().position.x - rb.GetComponent<Rigidbody2D>().position.x;
        float spacex = Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.GetComponent<Rigidbody2D>().position.x);
        float spacey = Mathf.Abs(player.GetComponent<Rigidbody2D>().position.y - rb.GetComponent<Rigidbody2D>().position.y);
        bool running = false;
        if (spacex <= detectionRange && spacey <= 7)
        {
            rb.MovePosition(rb.position + (move * speed * Time.deltaTime));
            running = true;
            if (distance < 0 && tr.transform.localScale.x > 0)
            {
                tr.transform.localScale *= new Vector2(-1, 1);
            }
            if (distance > 0 && tr.transform.localScale.x < 0)
            {
                tr.transform.localScale *= new Vector2(-1, 1);

            }
            if(spacex <= 1.5 && spacey <= 1)
            {
                running = false;
                anim.SetBool("isAttacking", true);
                StartCoroutine(waiter());
                if(spacex <= 1.5)
                {
                    player.GetComponent<PlayerHealth>().DecreaseHealth(10);
                }
            }
            else
            {
                anim.SetBool("isAttacking", false);
                
            }

        }
        
        if (running)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
    }
   
    public bool TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        return health <= 0;
    }


}

