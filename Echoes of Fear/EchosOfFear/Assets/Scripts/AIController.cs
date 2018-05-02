using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    //public Transform target;

    //public float moveSpeed = 3f;

    //public LayerMask raycastLayers;
    //public float rayDistance = 10f;

    //private Vector3 rayCollisionNormal;
    //private Vector3 hitLocationThisFrame = Vector3.zero;
    //private bool hitThisFrame = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBreaking = false

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //hitThisFrame = false;

        //RaycastHit hitInfo;
        //if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, raycastLayers.value))
        //{
        //    print("Raycast hit " + hitInfo.transform.name + " at " + hitInfo.point);
        //    rayCollisionNormal = hitInfo.normal;
        //    hitLocationThisFrame = hitInfo.point;
        //    hitThisFrame = true;
        //    transform.LookAt(target);
        //    transform.position = transform.forward * moveSpeed;
        //}
        //if (hitInfo.transform.tag == "Player")
        //{
        //    transform.LookAt(target);
        //    transform.position = transform.forward * Time.deltaTime * moveSpeed;
        //}
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

    }
}
