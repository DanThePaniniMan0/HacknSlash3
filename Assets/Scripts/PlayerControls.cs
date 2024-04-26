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
    string facing = "Right";

    public float health = 100;

    public GameManager manager;
    string equippedWeapon;
    public GameObject weapon;
    public Transform wTr;
    

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        equippedWeapon = manager.playerEquippedWeapon;
        wTr=weapon.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        //Movement Controls
        inAir = (rb.velocityY < -.01) || (rb.velocityY > .01);
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            if(!(rb.velocityX < (-1 * maxSpeed)))
                rb.AddForceX(thrust * -1, ForceMode2D.Force);
            if (facing != "Left" && wTr.GetComponent<WeaponBehavior>().SetFacing("Left"))
            {
                facing = "Left";
                wTr.localPosition = new Vector3(-1 * wTr.localPosition.x,wTr.localPosition.y,wTr.localPosition.z);
                wTr.eulerAngles = new Vector3(wTr.eulerAngles.x, wTr.eulerAngles.y, -1 * wTr.eulerAngles.z);
                
            }
            
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            if (!(rb.velocityX > maxSpeed))
                rb.AddForceX(thrust, ForceMode2D.Force);
            if (facing != "Right" && wTr.GetComponent<WeaponBehavior>().SetFacing("Right"))
            {
                facing = "Right";
                wTr.localPosition = new Vector3(-1 * wTr.localPosition.x, wTr.localPosition.y, wTr.localPosition.z);
                wTr.eulerAngles = new Vector3(wTr.eulerAngles.x, wTr.eulerAngles.y, -1 * wTr.eulerAngles.z);
                wTr.GetComponent<WeaponBehavior>().SetFacing(facing);
            }
        }
        else
        {
            rb.velocityX = 0;
        }
        if (Input.GetKey("w")&& !inAir){
            rb.AddForceY(jumpStrength, ForceMode2D.Impulse);
            inAir = true;
        }

        //Attack controls
        if (Input.GetMouseButtonDown(0))
        {
            weapon.GetComponent<WeaponBehavior>().Attack("");
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
            rb.velocityY = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 1;
        }
    }*/
}
