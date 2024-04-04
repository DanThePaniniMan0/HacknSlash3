using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed=5;
    public float jumpStrength=5;
    bool inAir=false;

    // Update is called once per frame
    void Update()
    {
        inAir = (rb.velocityY != 0);
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            rb.AddForceX(speed * -1, ForceMode2D.Force);
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            rb.AddForceX(speed, ForceMode2D.Force);
        }
        if (Input.GetKey("w")&& !inAir){
            rb.AddForceY(jumpStrength, ForceMode2D.Impulse);
            inAir = true;
        }
    }
}
