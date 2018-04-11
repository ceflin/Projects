using UnityEngine;
using System.Collections;


public class Agent : MonoBehaviour
{
    public float characterSpeed;  // how fast the character moves
    public Transform targetLocation;  //  where are we going

    private KinematicMove kineticMovementScript;  //  has all the movement logic

    public bool postProcessing = false;  //  pretty things up a bit

    public enum BehaviourType  //  allow the designer to select the behavior
    {
        Seek,
        Flee,
        Arrive,
        Wander
    };

    public BehaviourType behaviorSelection;

    //  if we want to activate "Arrive" we need for kinematic
    public float timeToTarget;
    public float satisfactionRadius;

    Animation anim;  //  used to get the animation compoment

    void Start()
    {
        kineticMovementScript = gameObject.GetComponent<KinematicMove>();
        anim = transform.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (behaviorSelection)
        {
            case BehaviourType.Seek:
                kineticMovementScript.CharacterSeek(targetLocation, characterSpeed);
                break;
            case BehaviourType.Flee:
                kineticMovementScript.CharacterFlee(targetLocation, characterSpeed);
                break;
            case BehaviourType.Arrive:
                kineticMovementScript.CharacterArrive(targetLocation, characterSpeed, timeToTarget, satisfactionRadius);
                break;
            case BehaviourType.Wander:
                kineticMovementScript.Wander(characterSpeed);
                break;
        }
	}

    //  perform any post movement adjustments
    void LateUpdate()
    {
        //  set us on the terrain
        if (postProcessing)
        {
            Vector3 pos = transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
            transform.position = pos;


            //  rotate the character to face movement direction
            

            Vector3 movementDirection = targetLocation.position - transform.position;

            if(behaviorSelection == BehaviourType.Flee)
            {
                movementDirection = -movementDirection;
            }

            Quaternion newRotation = Quaternion.LookRotation(movementDirection);

           


            // Set the player's rotation to this new rotation.
            GetComponent<Rigidbody>().MoveRotation(newRotation);

            if (movementDirection.magnitude > satisfactionRadius)
                anim.CrossFade("walk");
            else
                anim.CrossFade("idle");

            
        }

        


    }
}
