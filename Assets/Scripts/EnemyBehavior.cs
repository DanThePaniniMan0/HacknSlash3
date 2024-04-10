using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tr;
    public float detectionRange = 5;
    bool alert = false;
    public SpriteRenderer sp;
    GameObject player; 

    private void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
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
}
