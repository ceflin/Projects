using UnityEngine;
using System.Collections;

public class SteeringMove : MonoBehaviour
{
    public float gravity = 9;

    public void CharacterSeek(Transform target, float maxSpeed, float acceleration)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        //  get a vector to the target location
        Vector3 directionalVector = target.position - transform.position;
        directionalVector.Normalize();

        //  we need to have some mechanism for limiting the max speed of our character.  Lots of ways to do this.
        //  This way is going to mimic something like a gas peddle
        float currentSpeedDiffernce = maxSpeed - rb.velocity.magnitude;   //  how far are we from the speed we want to obtain
        float percentOfMax = currentSpeedDiffernce / maxSpeed;
        rb.AddForce(directionalVector * acceleration * percentOfMax);  //  this will add a force proportional to the differene in current vs target speed

        //  add gravity 
        rb.AddForce(-Vector3.up * gravity);

    }

    public void CharacterFlee(Transform target, float maxSpeed, float acceleration)
    {
       
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        //  get a vector to the target location
        Vector3 directionalVector = transform.position-target.position;
        directionalVector.Normalize();

        //  we need to have some mechanism for limiting the max speed of our character.  Lots of ways to do this.
        //  This way is going to mimic something like a gas peddle
        float currentSpeedDiffernce = maxSpeed - rb.velocity.magnitude;   //  how far are we from the speed we want to obtain
        float percentOfMax = currentSpeedDiffernce / maxSpeed;
        rb.AddForce(directionalVector * acceleration * percentOfMax);  //  this will add a force proportional to the differene in current vs target speed

        //  add gravity 
        rb.AddForce(-Vector3.up * gravity);

    }

    public void CharacterArrive(Transform target, float maxCharacterSpeed, float timeToTarget, float satisfactionRadius, float slowdownRadius, float maxAccleration)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float targetSpeed;  // 

        //  get a vector to the target location
        Vector3 directionalVector =  target.position-transform.position;
        
        //  determine which case we are in
        if(directionalVector.magnitude < satisfactionRadius)   //  we are there.  Just return
        {
            return;
        }

        if(directionalVector.magnitude > slowdownRadius)   //  outside the slow radius.  Try to go max speed
        {
            targetSpeed = maxCharacterSpeed;
        }
        else  //  Here we are between the two radii.  We need to calculate the scaled speed now
        {
            targetSpeed = maxCharacterSpeed * (directionalVector.magnitude / slowdownRadius);  //  speed proportional to distance from target (slow radius in this case)
        }

        //  now that we have a target speed, we want to apply the proper acceleration
        float currentSpeedDiffernce = targetSpeed - rb.velocity.magnitude;   //  how far are we from the speed we want to obtain
        float acceleration = currentSpeedDiffernce / timeToTarget;       //  a proportional acceleration

        //  make sure the computed acceleration doesn't exceed the max
        if (acceleration > maxAccleration)
        {
            acceleration = maxAccleration;
        }

        directionalVector.Normalize();
        rb.AddForce(directionalVector * acceleration);  //  this will add a force proportional to the differene in current vs target speed

        //  add gravity 
        rb.AddForce(-Vector3.up * gravity);


    }
}
