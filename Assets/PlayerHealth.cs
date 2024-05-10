using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float health = 200;

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
        yield return new WaitForSeconds(10);
        Destroy(player);
    }

    
}
