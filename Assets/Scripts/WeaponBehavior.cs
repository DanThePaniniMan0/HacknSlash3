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


    // Start is called before the first frame update
    void Start()
    {
        wielderTransform = wielder.GetComponent<Transform>();
        wielderCollider = wielder.GetComponent<BoxCollider2D>();

        weaponCollider = GetComponent<BoxCollider2D>();
        selfTransform = GetComponent<Transform>();

        originalRotation = selfTransform.eulerAngles.z;
        finalRotation = selfTransform.eulerAngles.z-sweepAngle;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pullingBack)
        {
            selfTransform.RotateAround(wielderTransform.position + new Vector3(0.5f, 0, 0),Vector3.forward,currentRotationVelocity);
        }else if (attacking)
        {
            selfTransform.RotateAround(wielderTransform.position + new Vector3(0.5f, 0, 0), Vector3.forward, -1*currentRotationVelocity);
            //Debug.Log($"{wielderTransform.position.x:f2}{wielderTransform.position.y:f2}{wielderTransform.position.z:f2}");
        }
        if (selfTransform.eulerAngles.z <= finalRotation)
        {
            pullingBack = true;
        }
        else if(attacking && selfTransform.eulerAngles.z >= originalRotation)
        {
            attacking = false;pullingBack = false;
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
