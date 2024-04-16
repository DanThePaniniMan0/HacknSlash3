using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    BoxCollider2D weilderCollider;
    Transform weilderTransform;

    BoxCollider2D weaponCollider;
    Transform selfTransform;
    //Rigidbody2D selfRigidbody;Vector3 offset;

    bool attacking = false;
    bool pullingBack = false;
    float rotationVelocity = 0;

    float originalRotation;
    float finalRotation;


    // Start is called before the first frame update
    void Start()
    {
        weilderCollider = GetComponentInParent<BoxCollider2D>();
        weilderTransform = GetComponentInParent<Transform>();
        weaponCollider = GetComponent<BoxCollider2D>();
        selfTransform = GetComponent<Transform>();
        //selfRigidbody = GetComponent<Rigidbody2D>();

        originalRotation = selfTransform.eulerAngles.z;
        finalRotation = selfTransform.eulerAngles.z-30;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pullingBack && originalRotation <= selfTransform.eulerAngles.z)
        {
            attacking = false; pullingBack = false;
            Debug.Log("Thing4");
        }
        else if (attacking && finalRotation <= selfTransform.eulerAngles.z)
        {
            //selfRigidbody.AddTorque(1, ForceMode2D.Force);
            selfTransform.Rotate(0, 0, -1*rotationVelocity);
            pullingBack = true; Debug.Log("Thing3");
        }
        else if (pullingBack)
        {
            selfTransform.Rotate(0, 0, rotationVelocity);Debug.Log("Thing2");
        }
        //I think u messed this (^^^) part up, might have to remake

    }

    public void Attack(string weaponMode){

        /*if(!attacking&&!pullingBack)
            selfRigidbody.AddTorque(1, ForceMode2D.Force);
        attacking = true;*/
        if (!attacking && !pullingBack)
        {
            rotationVelocity = 1;
            attacking = true;
        }
    }
}
