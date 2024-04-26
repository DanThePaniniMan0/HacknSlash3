using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    BoxCollider2D wielderCollider;
    Transform wielderTransform;
    public GameObject wielder;

    BoxCollider2D weaponCollider;
    Transform selfTransform;
    //Rigidbody2D selfRigidbody;Vector3 offset;

    bool attacking = false;
    bool pullingBack = false;
    public float rotationVelocity = 0.5f;
    float currentRotationVelocity;

    float originalRotation;
    float finalRotation;

    public float sweepAngle = 90;

    public float knockBack=5;
    public float damage = 50;

    public string facing="Right";



    // Start is called before the first frame update
    void Start()
    {
        wielderTransform = wielder.GetComponent<Transform>();
        wielderCollider = wielder.GetComponent<BoxCollider2D>();

        weaponCollider = GetComponent<BoxCollider2D>();
        selfTransform = GetComponent<Transform>();

        originalRotation = selfTransform.eulerAngles.z;
        finalRotation = originalRotation-sweepAngle;
       
    }

    // Update is called once per frame
    void Update()
    {
        //Add directions to attacking, need to enclose the whole thing in if statement, otherwise it will spin
        if (facing == "Right")
        {
            if (pullingBack)
            {
                selfTransform.RotateAround(wielderTransform.position + new Vector3(0.1f, 0, 0),Vector3.forward,currentRotationVelocity);
            }else if (attacking)
            {
                selfTransform.RotateAround(wielderTransform.position + new Vector3(0.1f, 0, 0), Vector3.forward, -1*currentRotationVelocity);
                //Debug.Log($"{wielderTransform.position.x:f2}{wielderTransform.position.y:f2}{wielderTransform.position.z:f2}");
            }
            if (attacking && selfTransform.eulerAngles.z <= finalRotation)
            {
                pullingBack = true;
                Debug.Log($"Euler Angle z: {selfTransform.eulerAngles.z}");
                Debug.Log($"Final Rotation Right:{finalRotation}");
            }
            else if(attacking && selfTransform.eulerAngles.z >= originalRotation)
            {
                attacking = false;pullingBack = false;
                Debug.Log($"Euler Angle z:{selfTransform.eulerAngles.z:f2}");
                Debug.Log($"Original Rotation Right:{originalRotation}");
            }

        }
        else if (facing == "Left")
        {
            if (pullingBack)
            {
                selfTransform.RotateAround(wielderTransform.position - new Vector3(0.1f, 0, 0), Vector3.forward, -1*currentRotationVelocity);
            }
            else if (attacking)
            {
                selfTransform.RotateAround(wielderTransform.position - new Vector3(0.1f, 0, 0), Vector3.forward, currentRotationVelocity);
                //Debug.Log($"{wielderTransform.position.x:f2}{wielderTransform.position.y:f2}{wielderTransform.position.z:f2}");
            }
            if (attacking && selfTransform.eulerAngles.z >= 360-finalRotation)
            {
                pullingBack = true;
                Debug.Log("Pulling Back Left Side");
                Debug.Log($"Euler Angle z:{selfTransform.eulerAngles.z:f2}");
                Debug.Log($"Final Rotation Left:{finalRotation}");
            }
            else if (attacking && selfTransform.eulerAngles.z <= 360-originalRotation)
            {
                attacking = false; pullingBack = false;
                Debug.Log("Attack Done Left Side");
                Debug.Log($"Euler Angle z:{selfTransform.eulerAngles.z:f2}");
                Debug.Log($"Original Rotation Left:{originalRotation}");
            }

        }
        else
        {
            Debug.Log("Weapon Behavior facing not recognized");
        }
    }
    public bool SetFacing(string f)
    {
        if (attacking)
        {
            return false;
        }
        facing = f;
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC" && attacking && !pullingBack)
        {
            if (collision.gameObject.transform.position.x > selfTransform.position.x)
            {
                collision.attachedRigidbody.AddForceX(knockBack, ForceMode2D.Impulse);
                collision.attachedRigidbody.AddForceY(knockBack, ForceMode2D.Impulse);
            }
            else
            {
                collision.attachedRigidbody.AddForceX(-knockBack, ForceMode2D.Impulse);
                collision.attachedRigidbody.AddForceY(-knockBack, ForceMode2D.Impulse);
            }
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }

    public void Attack(string weaponMode){

        
        if (!attacking && !pullingBack)
        {
            currentRotationVelocity = rotationVelocity;
            attacking = true;
        }
    }
}
