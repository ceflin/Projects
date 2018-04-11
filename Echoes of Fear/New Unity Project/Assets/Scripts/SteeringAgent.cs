using UnityEngine;
using System.Collections;

public class SteeringAgent : MonoBehaviour
{
    public float maxCharacterSpeed;  // how fast the character can move
    public float acceleration;       // basically how much force to apply
    public Transform targetLocation;  //  where are we going
    public float timeToTarget;          // used to calculate a target speed to arrive at the target in this amount of time
    public float satisfactionRadius;    // If we get to this radius from the target, we are considered "there"
    public float slowdownRadius;        // The point at which the character should begin to slow down

    private SteeringMove SteeringMovementScript;  //  has all the movement logic

    public bool postProcessing = false;  //  pretty things up a bit

    public enum BehaviourType  //  allow the designer to select the behavior
    {
        Seek,
        Flee,
        Arrive,
        Wander
    };

    Animation anim;  //  used to get the animation compoment

    public BehaviourType behaviorSelection;

    void Start()
    {
        SteeringMovementScript = gameObject.GetComponent<SteeringMove>();
        anim = transform.GetComponent<Animation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (behaviorSelection)
        {
            case BehaviourType.Seek:
                SteeringMovementScript.CharacterSeek(targetLocation, maxCharacterSpeed, acceleration);
                break;
            case BehaviourType.Flee:
                SteeringMovementScript.CharacterFlee(targetLocation, maxCharacterSpeed, acceleration);
                break;
            case BehaviourType.Arrive:
                SteeringMovementScript.CharacterArrive(targetLocation, maxCharacterSpeed, timeToTarget, satisfactionRadius, slowdownRadius, acceleration);
                break;
            case BehaviourType.Wander:
                //SteeringMovementScript.Wander(characterSpeed);
                break;
        }

    }

    //  perform any post movement adjustments
    void LateUpdate()
    {
        

        if (postProcessing)
        {
  

            // rotate towards movement direction.  As usual there are several ways to do this
            Vector3 relativeDirection = Vector3.forward;
            if (behaviorSelection == BehaviourType.Seek || behaviorSelection == BehaviourType.Arrive)
                relativeDirection = targetLocation.position - transform.position;
            else if(behaviorSelection == BehaviourType.Flee)
                relativeDirection =  transform.position- targetLocation.position;

           float rotationSpeed = 1f;
            Quaternion targetRotation = Quaternion.LookRotation(relativeDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb.velocity.magnitude > 0.5)
                anim.CrossFade("walk");
            else
                anim.CrossFade("idle");

            


        }
    }
}