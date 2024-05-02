using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D selfRigidBody;

    public float maxSpeed=5;
    public float thrust = 5;
    public float jumpStrength=5;
    bool inAir=false;
    string facing = "Right";

    public float health = 100;

    public GameManager manager;
    string equippedWeapon;
    public GameObject weapon;
    Transform weaponTransform;

    AudioSource jumpSFX;
    AudioSource runSFX;
    

    private void Start()
    {
        selfRigidBody= GetComponent<Rigidbody2D>();

        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        equippedWeapon = manager.playerEquippedWeapon;
        weaponTransform=weapon.GetComponent<Transform>();

        weapon.GetComponent<WeaponBehavior>().SetWeaponValues(equippedWeapon);

        runSFX= GetComponents<AudioSource>()[0];
        jumpSFX = GetComponents<AudioSource>()[1];
        
    }

    // Update is called once per frame
    void Update()
    {

        //Movement Controls
        inAir = (selfRigidBody.velocityY < -.01) || (selfRigidBody.velocityY > .01);
        if (inAir && runSFX.isPlaying)
        {
            runSFX.Stop();
        }
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            if(!(selfRigidBody.velocityX < (-1 * maxSpeed)))
                selfRigidBody.AddForceX(thrust * -1, ForceMode2D.Force);
            if (facing != "Left" && weaponTransform.GetComponent<WeaponBehavior>().SetFacing("Left"))
            {
                weaponTransform.GetComponent<WeaponBehavior>().SetFacing("Left");//
                facing = "Left";
                weaponTransform.localPosition = new Vector3(-1 * weaponTransform.localPosition.x,weaponTransform.localPosition.y,weaponTransform.localPosition.z);
                weaponTransform.eulerAngles = new Vector3(weaponTransform.eulerAngles.x, weaponTransform.eulerAngles.y, -1 * weaponTransform.eulerAngles.z);
                
            }
            if (!runSFX.isPlaying && !inAir)
            {
                runSFX.Play();
            }
            
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            if (!(selfRigidBody.velocityX > maxSpeed))
                selfRigidBody.AddForceX(thrust, ForceMode2D.Force);
            if (facing != "Right" && weaponTransform.GetComponent<WeaponBehavior>().SetFacing("Right"))
            {
                facing = "Right";
                weaponTransform.localPosition = new Vector3(-1 * weaponTransform.localPosition.x, weaponTransform.localPosition.y, weaponTransform.localPosition.z);
                weaponTransform.eulerAngles = new Vector3(weaponTransform.eulerAngles.x, weaponTransform.eulerAngles.y, -1 * weaponTransform.eulerAngles.z);
                weaponTransform.GetComponent<WeaponBehavior>().SetFacing(facing);
            }
            if (!runSFX.isPlaying && !inAir)
            {
                runSFX.Play();
            }
        }
        else
        {
            selfRigidBody.velocityX = 0;
            runSFX.Stop();
        }
        if (Input.GetKey("w")&& !inAir){
            selfRigidBody.AddForceY(jumpStrength, ForceMode2D.Impulse);
            inAir = true;
            jumpSFX.Play();
        }

        //Attack controls
        if (Input.GetMouseButtonDown(0))
        {
            weapon.GetComponent<WeaponBehavior>().Attack("");
        }
    }

    public void TakeDamage(float damageAmount,float xKnockback=0,float yKnockback=0)
    {
        selfRigidBody.AddForceX(xKnockback, ForceMode2D.Impulse);
        selfRigidBody.AddForceY(yKnockback, ForceMode2D.Impulse);
        health -= damageAmount;
        if (health <= 0)
        {

        }
    }
}
