using UnityEngine;
using System.Collections;

public class KinematicMove : MonoBehaviour
{
    public void CharacterSeek(Transform target, float speed)
    {
        //  get a vector to the target location
        Vector3 directionalVector = target.position - transform.position;
        //Debug.Log(directionalVector);
        directionalVector.Normalize();
        //DrawVector(directionalVector);

        //  calc the amount to move this frame
        directionalVector = directionalVector * speed * Time.deltaTime;

        //  move it
        transform.Translate(directionalVector, Space.World);

    }

    public void CharacterFlee(Transform target, float speed)
    {
        //  get a vector to the target location
        Vector3 directionalVector = transform.position - target.position;
        directionalVector.Normalize();

        //  calc the amount to move this frame
        directionalVector = directionalVector * speed * Time.deltaTime;

        //  move it
        transform.Translate(directionalVector, Space.World);

    }

    public void CharacterArrive(Transform target, float speed, float arriveTime, float satisfactionRadius)
    {
        //  get a vector to the target location
        Vector3 directionalVector = target.position - transform.position;

        //  check to see if we are already at the target
        if (directionalVector.magnitude < satisfactionRadius)
            return;

        //  otherwiswe calc the speed we would need to go to get to the target within the arrivalTime
        float calculatedSpeed = directionalVector.magnitude / arriveTime;

        //  if the speed is too fast we need to clip it to the max
        if (calculatedSpeed > speed)
            calculatedSpeed = speed;

        //  now do the normal kinematic move with calculatedSpeed
        directionalVector.Normalize();

        //  calc the amount to move this frame
        directionalVector = directionalVector * calculatedSpeed * Time.deltaTime;

        //  move it
        transform.Translate(directionalVector, Space.World);
    }

    public void Wander(float speed)
    {
        float degreesToTurn;

        degreesToTurn = Random.Range(-1.0f, 1.0f) - Random.Range(-1.0f, 1.0f) * 5;

        transform.Rotate(0, degreesToTurn, 0);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //Debug.Log("Degrees to turn" + degreesToTurn);
        //Debug.Log("In wander routine");
    }

    private void DrawVector(Vector3 vectorToDraw)
    {
        Debug.Log("Vector to draw " + vectorToDraw);
        Vector3 endpoint = new Vector3(transform.position.x + vectorToDraw.x, transform.position.y + vectorToDraw.y, transform.position.z + vectorToDraw.z);
        Debug.DrawLine(transform.position,   endpoint);
    }


}
