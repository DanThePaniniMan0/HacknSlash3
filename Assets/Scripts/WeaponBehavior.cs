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
    float currentAttackSpeed;

    float originalRotation;
    float finalRotation;
    Vector3 offsetVector;

    public float sweepAngle = 120;
    public float baseScale = 1f;

    float knockBack=5;
    float damage = 50;
    float attackSpeed = 0.5f;

    string facing="Right";


    AudioSource swordSFX;
    AudioSource daggerSFX;
    AudioSource fistSFX;
    AudioSource selectedWeaponSFX;


    // Start is called before the first frame update
    void Start()
    {
        wielderTransform = wielder.GetComponent<Transform>();
        wielderCollider = wielder.GetComponent<BoxCollider2D>();

        weaponCollider = GetComponent<BoxCollider2D>();
        selfTransform = GetComponent<Transform>();

        originalRotation = selfTransform.eulerAngles.z;
        finalRotation = originalRotation-sweepAngle;
        offsetVector=new Vector3(selfTransform.localPosition.x,0,0);

        AudioSource[] SFXList = GetComponents<AudioSource>();
        swordSFX = SFXList[0];
        daggerSFX = SFXList[1];
        fistSFX = SFXList[2];
        selectedWeaponSFX = swordSFX;
    }

    public void SetWeaponValues(string weaponType)
    {
        switch (weaponType)
        {
            case ("Sword"):
                attackSpeed = 0.5f;
                knockBack = 2;
                damage = 50;
                selfTransform.localScale = new Vector3(0.2f, 1.5f*baseScale, 1);
                selectedWeaponSFX = swordSFX;
                break;
            case ("Dagger"):
                attackSpeed = 1f;
                knockBack = 2;
                damage = 25;
                selfTransform.localScale = new Vector3(0.2f, 0.8f*baseScale, 1);
                selectedWeaponSFX = daggerSFX;
                break;
            case ("Fist"):
                attackSpeed = 2f;
                knockBack = 5;
                damage = 10;
                selfTransform.localScale = new Vector3(0.2f, 0.5f*baseScale, 1);
                selectedWeaponSFX= fistSFX;
                break;
            default:
                Debug.Log("Weapon Behavior : SetWeaponValues: Unrecognized weapon type");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Add directions to attacking, need to enclose the whole thing in if statement, otherwise it will spin
        if (facing == "Right")
        {
            if (pullingBack)
            {
                selfTransform.RotateAround(wielderTransform.position + offsetVector,Vector3.forward,currentAttackSpeed);
            }else if (attacking)
            {
                selfTransform.RotateAround(wielderTransform.position + offsetVector, Vector3.forward, -1*currentAttackSpeed);
                //Debug.Log($"{wielderTransform.position.x:f2}{wielderTransform.position.y:f2}{wielderTransform.position.z:f2}");
            }
            if (attacking && selfTransform.eulerAngles.z <= finalRotation)
            {
                pullingBack = true;
            }
            else if(attacking && selfTransform.eulerAngles.z >= originalRotation)
            {
                attacking = false;pullingBack = false;
            }

        }
        else if (facing == "Left")
        {
            if (pullingBack)
            {
                selfTransform.RotateAround(wielderTransform.position - offsetVector, Vector3.forward, -1*currentAttackSpeed);
            }
            else if (attacking)
            {
                selfTransform.RotateAround(wielderTransform.position - offsetVector, Vector3.forward, currentAttackSpeed);
                //Debug.Log($"{wielderTransform.position.x:f2}{wielderTransform.position.y:f2}{wielderTransform.position.z:f2}");
            }
            if (attacking && selfTransform.eulerAngles.z >= 360-finalRotation)
            {
                pullingBack = true;
            }
            else if (attacking && selfTransform.eulerAngles.z <= 360-originalRotation)
            {
                attacking = false; pullingBack = false;
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
                collision.attachedRigidbody.AddForceY(1, ForceMode2D.Impulse);
            }
            else
            {
                collision.attachedRigidbody.AddForceX(-knockBack, ForceMode2D.Impulse);
                collision.attachedRigidbody.AddForceY(1, ForceMode2D.Impulse);
            }
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }

    public void Attack(string weaponMode){

        
        if (!attacking && !pullingBack)
        {
            currentAttackSpeed = attackSpeed;
            attacking = true;
            selectedWeaponSFX.Play();
        }
    }
}
