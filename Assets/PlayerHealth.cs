using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Jobs;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float health = 200;
    public GameObject rb;


    public void DecreaseHealth(float damage)
    {
        this.health = this.health - damage;

        if (health <= 0)
        {
            StartCoroutine(waiter());
            
        }
    }

    IEnumerator waiter()
    {
        anim.Play("FighterDead");
        player.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y - 0.3f, player.GetComponent<Transform>().position.z);

        yield return new WaitForSeconds(10);
        Destroy(player);
    }
    private void Start()
    {
        
    }
    void Update()
    {
        float distance = Math.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.GetComponent<Rigidbody2D>().position.x);
        if(distance < 2)
        {
            anim.SetBool("IsAttacking", true);
            rb.GetComponent<EnemyBehavior>().TakeDamage(10);

        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}
