using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed=5;
    public float thrust = 5;
    public float jumpStrength=5;
    bool inAir=false;

    // Update is called once per frame
    void Update()
    {
        inAir = (rb.velocityY < -.01) || (rb.velocityY > .01);
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            if(!(rb.velocityX < (-1 * maxSpeed)))
                rb.AddForceX(thrust * -1, ForceMode2D.Force);
            
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            if (!(rb.velocityX > maxSpeed))
                rb.AddForceX(thrust, ForceMode2D.Force);
        }
        if (Input.GetKey("w")&& !inAir){
            rb.AddForceY(jumpStrength, ForceMode2D.Impulse);
            inAir = true;
        }
    }
}
